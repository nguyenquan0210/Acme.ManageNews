using Acme.ManageNews.Entities;
using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.ManageNews.Catalog.Topics
{
    public class TopicManager : DomainService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicManager(ITopicRepository TopicRepository)
        {
            _topicRepository = TopicRepository;
        }

        public async Task<Topic> CreateAsync(
             [NotNull] string name,
            Status Status,
            int SortOrder,
            bool Hot)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingTopic = await _topicRepository.FindByNameAsync(name);
            if (existingTopic != null)
            {
                throw new CatalogAlreadyExistsException(name);
            }

            return new Topic(
                GuidGenerator.Create(),
                name,
                Status,
                SortOrder,
                Hot
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Topic Topic,
            [NotNull] string newName)
        {
            Check.NotNull(Topic, nameof(Topic));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingTopic = await _topicRepository.FindByNameAsync(newName);
            if (existingTopic != null && existingTopic.Id != Topic.Id)
            {
                throw new CatalogAlreadyExistsException(newName);
            }

            Topic.ChangeName(newName);
        }
    }
}
