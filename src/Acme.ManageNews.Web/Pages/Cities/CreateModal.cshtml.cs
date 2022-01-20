using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.ManageNews.Web.Pages.Cities
{
    public class CreateModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public CreateCityViewModel City { get; set; }

        private readonly ICityAppService _cityAppService;

        public CreateModalModel(ICityAppService cityAppService)
        {
            _cityAppService = cityAppService;
        }

        public void OnGet()
        {
            City = new CreateCityViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateCityViewModel, CreateCityDto>(City);
            await _cityAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateCityViewModel
        {
            [Required]
            [StringLength(CatalogConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            public Status Status { get; set; } = Status.Active;

            [Required]
            public int SortOrder { get; set; } = 1;
        }
    }
}
