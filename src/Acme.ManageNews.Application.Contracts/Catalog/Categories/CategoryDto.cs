using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog.Categories
{
    public class CategoryDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }
    }
}
