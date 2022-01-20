using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class Events : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public Status Status { get; set; }

        public Guid CategoryId { get; set; }

        public bool Hot { get; set; }

        public int SortOrder { get; set; }

        private Events()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Events(
            Guid id,
            Guid categoryId,
            [NotNull] string name,
            Status status,
            int sortOrder,
            bool hot)
            : base(id)
        {
            SetName(name);
            CategoryId = categoryId;
            Status = status;
            SortOrder = sortOrder;
            Hot = hot;
        }

        internal Events ChangeName([NotNull] string name)
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
