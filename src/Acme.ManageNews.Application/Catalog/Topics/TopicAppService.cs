using Acme.ManageNews.Entities;
using Acme.ManageNews.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Topics
{
    [Authorize(ManageNewsPermissions.Cities.Default)]
    public class TopicAppService : ManageNewsAppService, ITopicAppService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly TopicManager _topicManager;

        public TopicAppService(
            ITopicRepository topicRepository,
            TopicManager topicManager)
        {
            _topicRepository = topicRepository;
            _topicManager = topicManager;
        }

        [Authorize(ManageNewsPermissions.Categories.Create)]
        public async Task<TopicDto> CreateAsync(CreateTopicDto input)
        {
            var Topic = await _topicManager.CreateAsync(
                input.Name,
                input.Status = Enums.Status.Active,
                input.SortOrder,
                input.Hot
            );

            await _topicRepository.InsertAsync(Topic);

            return ObjectMapper.Map<Topic, TopicDto>(Topic);
        }

        [Authorize(ManageNewsPermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _topicRepository.DeleteAsync(id);
        }

        public async Task<TopicDto> GetAsync(Guid id)
        {
            var topic = await _topicRepository.GetAsync(id);
            return ObjectMapper.Map<Topic, TopicDto>(topic);
        }

        public async Task<PagedResultDto<TopicDto>> GetListAsync(GetCatalogListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Topic.Name);
            }

            var topics = await _topicRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _topicRepository.CountAsync()
                : await _topicRepository.CountAsync(
                    x => x.Name.Contains(input.Filter));

            return new PagedResultDto<TopicDto>(
                totalCount,
                ObjectMapper.Map<List<Topic>, List<TopicDto>>(topics)
            );
        }

        [Authorize(ManageNewsPermissions.Categories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateTopicDto input)
        {
            var Topic = await _topicRepository.GetAsync(id);

            if (Topic.Name != input.Name)
            {
                await _topicManager.ChangeNameAsync(Topic, input.Name);
            }

            Topic.SortOrder = input.SortOrder;
            Topic.Status = input.Status;
            Topic.Hot = input.Hot;

            await _topicRepository.UpdateAsync(Topic);
        }

        //...SERVICE METHODS WILL COME HERE...
    }
}
