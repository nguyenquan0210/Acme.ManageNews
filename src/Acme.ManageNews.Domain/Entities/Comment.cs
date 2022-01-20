using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Comment : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid NewsId { get; set; }

        public string Title { get; set; }

    }
}
