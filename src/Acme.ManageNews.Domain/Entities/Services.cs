using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Services : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Period { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

    }
}
