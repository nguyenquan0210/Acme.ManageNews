using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Rating : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid NewsId { get; set; }

        public string Checkrating { get; set; }

        public int Value { get; set; }
    }
}
