using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.ManageNews.Catalog.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(CatalogConsts.MaxNameLength)]
        public string Name { get; set; }
        
        public Status Status { get; set; }
        [Required]
        public int SortOrder { get; set; }
    }
}
