using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public string Title { get; set; }

        public OrderStatus Status { get; set; }

    }
}
