using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog
{
    public class GetCatalogListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
