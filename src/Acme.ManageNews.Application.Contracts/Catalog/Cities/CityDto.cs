using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog.Cities
{
    public class CityDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }
    }
}
