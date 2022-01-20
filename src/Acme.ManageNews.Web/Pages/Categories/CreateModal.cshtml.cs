using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.ManageNews.Web.Pages.Categories
{
    public class CreateModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public CreateCategoryViewModel Category { get; set; }

        private readonly ICategoryAppService _categoryAppService;

        public CreateModalModel(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public void OnGet()
        {
            Category = new CreateCategoryViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateCategoryViewModel, CreateCategoryDto>(Category);
            await _categoryAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateCategoryViewModel
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

