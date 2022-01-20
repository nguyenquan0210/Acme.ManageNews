using Acme.ManageNews.Catalog.Categories;
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

namespace Acme.ManageNews.Catalog.Eventss
{
    [Authorize(ManageNewsPermissions.Events.Default)]
    public class EventsAppService : ManageNewsAppService, IEventsAppService //implement the IBookAppService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly EventsManager _eventsManager;

        private readonly ICategoryRepository _categoryRepository;

        public EventsAppService(IEventsRepository eventsRepository, EventsManager eventsManager,
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _eventsRepository = eventsRepository;
            _eventsManager = eventsManager;
        }

        [Authorize(ManageNewsPermissions.Categories.Create)]
        public async Task<EventsDto> CreateAsync(CreateEventsDto input)
        {
            var events = await _eventsManager.CreateAsync(
                input.Name,
                input.Status = Enums.Status.Active,
                input.SortOrder,
                input.Hot,
                input.CategoryId
            );

            await _eventsRepository.InsertAsync(events);

            return ObjectMapper.Map<Events, EventsDto>(events);
        }

        [Authorize(ManageNewsPermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _eventsRepository.DeleteAsync(id);
        }

        public async Task<EventsDto> GetAsync(Guid id)
        {
            var Events = await _eventsRepository.GetAsync(id);

            return ObjectMapper.Map<Events, EventsDto>(Events);
        }

        public async Task<PagedResultDto<EventsDto>> GetListAsync(GetCatalogListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Events.Name);
            }

            var queryable = await _eventsRepository.GetQueryableAsync();

            var query = from events in queryable
                        join category in await _categoryRepository.GetQueryableAsync() on events.CategoryId equals category.Id
                        select new { events, category };

            query = query.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    x => x.events.Name.Contains(input.Filter)
                 )
                .OrderBy(x => x.events.SortOrder)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            var eventsDtos = queryResult.Select(x =>
            {
                var eventsDto = ObjectMapper.Map<Events, EventsDto>(x.events);
                eventsDto.CategoryName = x.category.Name;
                return eventsDto;
            }).ToList();

            var totalCount = input.Filter == null
                ? await _eventsRepository.CountAsync()
                : await _eventsRepository.CountAsync(
                    x => x.Name.Contains(input.Filter));
            return new PagedResultDto<EventsDto>(
               totalCount,
               eventsDtos
           );
        }

        [Authorize(ManageNewsPermissions.Categories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateEventsDto input)
        {
            var Events = await _eventsRepository.GetAsync(id);

            if (Events.Name != input.Name)
            {
                await _eventsManager.ChangeNameAsync(Events, input.Name);
            }

            Events.SortOrder = input.SortOrder;
            Events.Status = input.Status;
            Events.CategoryId = input.CategoryId;
            Events.Hot = input.Hot;

            await _eventsRepository.UpdateAsync(Events);
        }

        public async Task<ListResultDto<CatalogLookupDto>> GetCategoryLookupAsync()
        {
            var categories = await _categoryRepository.GetListAsync();

            return new ListResultDto<CatalogLookupDto>(
                ObjectMapper.Map<List<Category>, List<CatalogLookupDto>>(categories)
            );
        }
        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"events.{nameof(Events.Name)}";
            }

            if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "categoryName",
                    "category.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"events.{sorting}";
        }

        //...SERVICE METHODS WILL COME HERE...
    }
}
