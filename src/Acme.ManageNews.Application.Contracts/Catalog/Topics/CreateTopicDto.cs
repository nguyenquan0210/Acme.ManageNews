﻿using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using System.ComponentModel.DataAnnotations;

namespace Acme.ManageNews.Catalog.Topics
{
    public class CreateTopicDto
    {
        [Required]
        [StringLength(CatalogConsts.MaxNameLength)]
        public string Name { get; set; }
        
        public Status Status { get; set; }
        [Required]
        public int SortOrder { get; set; }
        [Required]
        public bool Hot { get; set; }
    }
}