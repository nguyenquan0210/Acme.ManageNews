using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalog.Eventss;
using Acme.ManageNews.Catalog.Topics;
using Acme.ManageNews.Entities;
using Acme.ManageNews.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Acme.ManageNews.Catalog.Newss
{
    [Authorize(ManageNewsPermissions.Categories.Default)]
    public class NewsAppService : ManageNewsAppService,
        INewsAppService //implement the INewsAppService
    {
        private readonly INewsRepository _newsRepository;
        private readonly NewsManager _newsManager;
        private readonly IEventsRepository _eventsRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IdentityUserAppService _identityUserAppService;

        public NewsAppService(INewsRepository NewsRepository, NewsManager NewsManager,
            IRepository<News, Guid> repository,
            IEventsRepository eventsRepository,
            ICityRepository cityRepository,
            ITopicRepository topicRepository,
            ICategoryRepository categoryRepository,
            ICurrentUser currentUser,
            IdentityUserAppService identityUserAppService)
        {
            _currentUser = currentUser;
            _eventsRepository = eventsRepository;
            _cityRepository = cityRepository;
            _topicRepository = topicRepository;
            _categoryRepository = categoryRepository;
            _identityUserAppService = identityUserAppService;
            _newsRepository = NewsRepository;
            _newsManager = NewsManager;

        }
        [Authorize(ManageNewsPermissions.Categories.Create)]
        public async Task<NewsDto> CreateAsync(CreateNewsDto input)
        {
            var News =  _newsManager.CreateAsync(
                input.Title,
                input.Description,
                input.Content,
                input.Status,
                input.NewsHot,
                input.Img,
                input.Viewss,
                input.Keyword,
                input.CityId,
                input.TopicId,
                input.EventId,
                input.UserId = (Guid)_currentUser.Id,input.Video,input.Url
            );

            await _newsRepository.InsertAsync(News);

            return ObjectMapper.Map<News, NewsDto>(News);
        }

        [Authorize(ManageNewsPermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _newsRepository.DeleteAsync(id);
        }

        public async Task<NewsDto> GetAsync(Guid id)
        {
            var News = await _newsRepository.GetAsync(id);

            return ObjectMapper.Map<News, NewsDto>(News);
        }

        public async Task<PagedResultDto<NewsDto>> GetListAsync(GetCatalogListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(News.Title);
            }

            var queryable = await _newsRepository.GetQueryableAsync();

            var query = from News in queryable
                        join Events in await _eventsRepository.GetQueryableAsync() on News.EventId equals Events.Id
                        join Category in await _categoryRepository.GetQueryableAsync() on Events.CategoryId equals Category.Id
                        join City in await _cityRepository.GetQueryableAsync() on News.CityId equals City.Id
                        join Topic in await _topicRepository.GetQueryableAsync() on News.TopicId equals Topic.Id
                        select new { News, Category };


            query = query.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    x => x.News.Title.Contains(input.Filter)
                 ).WhereIf(!_currentUser.IsInRole("admin"),x=>x.News.UserId == _currentUser.Id)
                .OrderByDescending(x => x.News.DeletionTime)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            var NewsDtos = queryResult.Select(x =>
            {
                var NewsDto = ObjectMapper.Map<News, NewsDto>(x.News);
                NewsDto.CategoryName = x.Category.Name;
                NewsDto.UserName = _identityUserAppService.GetAsync(x.News.UserId).Result.UserName;
                return NewsDto;
            }).ToList();

            var totalCount = input.Filter == null
                ? await _newsRepository.CountAsync()
                : await _newsRepository.CountAsync(
                    x => x.Title.Contains(input.Filter));
            return new PagedResultDto<NewsDto>(
               totalCount,
               NewsDtos
           );
        }

        [Authorize(ManageNewsPermissions.Categories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateNewsDto input)
        {
            var News = await _newsRepository.GetAsync(id);

            News.Title = input.Title;
            News.Status = input.Status;
            News.Description = input.Description;
            News.Content = input.Content;
            News.Img = input.Img;
            News.Keyword = input.Keyword;
            News.CityId = input.CityId;
            News.TopicId = input.TopicId;
            News.EventId = input.EventId;
            await _newsRepository.UpdateAsync(News);
        }
        /*public  async Task<NewsDto> GetAsync(Guid id)
        {
            //Get the IQueryable<News> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join Newss and authors
            var query = from News in queryable
                        join Events in await _newsRepository.GetQueryableAsync() on News.EventId equals News.Id
                        join Category in await _categoryRepository.GetQueryableAsync() on News.CategoryId equals Category.Id
                        join City in await _cityRepository.GetQueryableAsync() on News.CityId equals City.Id
                        join Topic in await _topicRepository.GetQueryableAsync() on News.TopicId equals Topic.Id
                        where News.Id == id
                        select new { News, Category };

            //Execute the query and get the News with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(News), id);
            }

            var NewsDto = ObjectMapper.Map<News, NewsDto>(queryResult.News);
            NewsDto.CategoryName = queryResult.Category.Name;
            return NewsDto;
        }*/

        /*public override async Task<PagedResultDto<NewsDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<News> from the repository
            var queryable = await Repository.GetQueryableAsync();


            //Prepare a query to join Newss and authors
            var query = from News in queryable
                        join News in await _newsRepository.GetQueryableAsync() on News.EventId equals News.Id
                        join Category in await _categoryRepository.GetQueryableAsync() on News.CategoryId equals Category.Id
                        join City in await _cityRepository.GetQueryableAsync() on News.CityId equals City.Id
                        join Topic in await _topicRepository.GetQueryableAsync() on News.TopicId equals Topic.Id
                        select new { News, News, Category, City, Topic };

            //Paging
            query = query
                .OrderBy(x => x.News.Title)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of NewsDto objects
            var NewsDtos = queryResult.Select(x =>
            {
                var NewsDto = ObjectMapper.Map<News, NewsDto>(x.News);
                NewsDto.CategoryName = x.Category.Name;
                NewsDto.UserName = _identityUserAppService.GetAsync(x.News.UserId).Result.UserName;
                return NewsDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<NewsDto>(
                totalCount,
                NewsDtos
            );
        }*/
       

        public async Task<ListResultDto<CatalogLookupDto>> GetEventsLookupAsync()
        {
            var events = await _eventsRepository.GetListAsync();

            return new ListResultDto<CatalogLookupDto>(
                ObjectMapper.Map<List<Events>, List<CatalogLookupDto>>(events)
            );
        }
        public async Task<ListResultDto<CatalogLookupDto>> GetCityLookupAsync()
        {
            var cities = await _cityRepository.GetListAsync();

            return new ListResultDto<CatalogLookupDto>(
                ObjectMapper.Map<List<City>, List<CatalogLookupDto>>(cities)
            );
        }
        public async Task<ListResultDto<CatalogLookupDto>> GetTopicLookupAsync()
        {
            var topics = await _topicRepository.GetListAsync();

            return new ListResultDto<CatalogLookupDto>(
                ObjectMapper.Map<List<Topic>, List<CatalogLookupDto>>(topics)
            );
        }


        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"News.{nameof(News.Title)}";
            }

            if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "categoryName",
                    "category.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"News.{sorting}";
        }
    }
}
