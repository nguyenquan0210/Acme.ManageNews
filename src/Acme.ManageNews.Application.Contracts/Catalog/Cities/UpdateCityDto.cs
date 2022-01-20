using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using System.ComponentModel.DataAnnotations;

namespace Acme.ManageNews.Catalog.Cities
{
    public class UpdateCityDto
    {
        [Required]
        [StringLength(CatalogConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public int SortOrder { get; set; }
    }
}