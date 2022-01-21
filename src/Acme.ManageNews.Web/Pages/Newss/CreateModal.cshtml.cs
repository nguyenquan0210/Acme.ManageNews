using Acme.ManageNews.Catalog.Newss;
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

namespace Acme.ManageNews.Web.Pages.Newss
{
    public class CreateModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public CreateNewsViewModel News { get; set; }

        public List<SelectListItem> Events { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Topics { get; set; }

        private readonly INewsAppService _newsAppService;

        public CreateModalModel(INewsAppService NewsAppService)
        {
            _newsAppService = NewsAppService;
        }

        public async void OnGet()
        {
            News = new CreateNewsViewModel();

            var eventsLookup = await _newsAppService.GetEventsLookupAsync();
            Events = eventsLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var topicLookup = await _newsAppService.GetTopicLookupAsync();
            Topics = topicLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var cityLookup = await _newsAppService.GetCityLookupAsync();
            Cities = cityLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateNewsViewModel, CreateNewsDto>(News);
            await _newsAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateNewsViewModel
        {
            [Required]
            [StringLength(255)]
            public string Title { get; set; }
            [Required]
            [StringLength(255)]
            public string Description { get; set; }
            [Required]
            [StringLength(int.MaxValue)]
            public string Content { get; set; }
            [Required]
            [StringLength(100)]
            public string Img { get; set; }
            [Required]
            public Status NewsHot { get; set; }
            [Required]
            public Status Status { get; set; }
            [Required]
            [StringLength(255)]
            public string Keyword { get; set; }

            [SelectItems(nameof(Events))]
            [DisplayName("Events")]
            public Guid EventId { get; set; }
            [SelectItems(nameof(Cities))]
            [DisplayName("City")]
            public Guid CityId { get; set; }
            [SelectItems(nameof(Topics))]
            [DisplayName("Topic")]
            public Guid TopicId { get; set; }

        }
    }
}
