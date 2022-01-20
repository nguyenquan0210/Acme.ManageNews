using System;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog
{
    public class CatalogLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}