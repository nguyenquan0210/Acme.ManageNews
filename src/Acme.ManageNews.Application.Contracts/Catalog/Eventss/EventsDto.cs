using Acme.ManageNews.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog.Eventss
{
    public class EventsDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }

        public bool Hot { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}