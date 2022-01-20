using Acme.ManageNews.Enums;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Advertise : FullAuditedAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlImg { get; set; }

        public DateTime Published_Date { get; set; }

        public DateTime Expire_Date { get; set; }

        public Status Status { get; set; }
    }
}
