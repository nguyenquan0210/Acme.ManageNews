using Acme.ManageNews.Catalog.Eventss;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.ManageNews.Web.Pages.Eventss
{
    public class CreateModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public CreateEventsViewModel Events { get; set; }

        public List<SelectListItem> Categories { get; set; }


        private readonly IEventsAppService _eventsAppService;

        public CreateModalModel(IEventsAppService EventsAppService)
        {
            _eventsAppService = EventsAppService;
        }

        public async void OnGet()
        {
            Events = new CreateEventsViewModel();

            var categoryLookup = await _eventsAppService.GetCategoryLookupAsync();
            Categories = categoryLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _eventsAppService.CreateAsync(
                ObjectMapper.Map<CreateEventsViewModel, CreateEventsDto>(Events)
                );
            return NoContent();
        }
        public class CreateEventsViewModel
        {
            [SelectItems(nameof(Categories))]
            [DisplayName("Category")]
            public Guid CategoryId { get; set; }

            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public int SortOrder { get; set; } = 1;
            [Required]
            public bool Hot { get; set; } = false;

        }
    }
}
