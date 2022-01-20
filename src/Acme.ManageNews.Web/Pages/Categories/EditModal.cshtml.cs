using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Entities;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.ManageNews.Web.Pages.Categories
{
    public class EditModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public EditCategoryViewModel Category { get; set; }

        private readonly ICategoryAppService _categoryAppService;

        public EditModalModel(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var CategoryDto = await _categoryAppService.GetAsync(id);
            Category = ObjectMapper.Map<CategoryDto, EditCategoryViewModel>(CategoryDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _categoryAppService.UpdateAsync(
                Category.Id,
                ObjectMapper.Map<EditCategoryViewModel, UpdateCategoryDto>(Category)
            );

            return NoContent();
        }

        public class EditCategoryViewModel
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
