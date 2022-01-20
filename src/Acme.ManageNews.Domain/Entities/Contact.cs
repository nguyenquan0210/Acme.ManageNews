using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Contact : FullAuditedAggregateRoot<Guid>
    {
        public string Company { get; set; }

        public string Leader { get; set; }

        public string Position { get; set; }

        public string License { get; set; }

        public string Email { get; set; }

        public string Hotline { get; set; }

        public string Address { get; set; }

        public string ContactAdvertise { get; set; }

    }
}
