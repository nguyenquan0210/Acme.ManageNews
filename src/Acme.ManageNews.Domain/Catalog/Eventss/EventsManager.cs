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

namespace Acme.ManageNews.Catalog.Eventss
{
    public class EventsManager : DomainService
    {
        private readonly IEventsRepository _eventsRepository;

        public EventsManager(IEventsRepository EventsRepository)
        {
            _eventsRepository = EventsRepository;
        }

        public async Task<Events> CreateAsync(
             [NotNull] string name,
            Status Status,
            int SortOrder,
            bool Hot,
            Guid CategoryId)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingEvents = await _eventsRepository.FindByNameAsync(name);
            if (existingEvents != null)
            {
                throw new CatalogAlreadyExistsException(name);
            }

            return new Events(
                GuidGenerator.Create(),
                CategoryId,
                name,
                Status,
                SortOrder,
                Hot
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Events Events,
            [NotNull] string newName)
        {
            Check.NotNull(Events, nameof(Events));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingEvents = await _eventsRepository.FindByNameAsync(newName);
            if (existingEvents != null && existingEvents.Id != Events.Id)
            {
                throw new CatalogAlreadyExistsException(newName);
            }

            Events.ChangeName(newName);
        }
    }
}
