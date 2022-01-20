using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.ManageNews.Web.Pages.Cities
{
    public class EditModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public EditCityViewModel City { get; set; }

        private readonly ICityAppService _cityAppService;

        public EditModalModel(ICityAppService cityAppService)
        {
            _cityAppService = cityAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var CityDto = await _cityAppService.GetAsync(id);
            City = ObjectMapper.Map<CityDto, EditCityViewModel>(CityDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _cityAppService.UpdateAsync(
                City.Id,
                ObjectMapper.Map<EditCityViewModel, UpdateCityDto>(City)
            );

            return NoContent();
        }

        public class EditCityViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(CatalogConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            public Status Status { get; set; }

            [Required]
            public int SortOrder { get; set; }
        }
    }
}
