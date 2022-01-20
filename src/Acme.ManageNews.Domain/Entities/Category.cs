using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }

        private Category()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Category(
            Guid id,
            [NotNull] string name,
            Status status,
            int sortOrder)
            :base(id)
        {
            SetName(name);
            Status = status;
            SortOrder = sortOrder;
        }

        internal Category ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: CatalogConsts.MaxNameLength
            );
        }
    }
}
