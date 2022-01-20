using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Save : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid NewsId { get; set; }

        public string check { get; set; }

    }
}
