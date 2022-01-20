using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.ManageNews.Catalog.Eventss
{
    public class CreateEventsDto
    {
        [Required]
        [StringLength(CatalogConsts.MaxNameLength)]
        public string Name { get; set; }
        public Status Status { get; set; } = Status.Active;
        [Required]
        public int SortOrder { get; set; } = 1;
        [Required]
        public bool Hot { get; set; } = false;
        [Required]
        public Guid CategoryId { get; set; }
    }
}