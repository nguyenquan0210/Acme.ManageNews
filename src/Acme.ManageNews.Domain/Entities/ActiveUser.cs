using Acme.ManageNews.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class ActiveUser : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public DateTime DateActive { get; set; }

        public TimeSpan? TimeActive { get; set; }

        public int AccessSuccessCount { get; set; }

        
    }
}
