using Acme.ManageNews.Catalog.Topics;
using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.ManageNews.Web.Pages.Topics
{
    public class EditModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public EditTopicViewModel Topic { get; set; }

        private readonly ITopicAppService _topicAppService;

        public EditModalModel(ITopicAppService TopicAppService)
        {
            _topicAppService = TopicAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var TopicDto = await _topicAppService.GetAsync(id);
            Topic = ObjectMapper.Map<TopicDto, EditTopicViewModel>(TopicDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _topicAppService.UpdateAsync(
                Topic.Id,
                ObjectMapper.Map<EditTopicViewModel, UpdateTopicDto>(Topic)
            );

            return NoContent();
        }

        public class EditTopicViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [Required]
            [StringLength(CatalogConsts.MaxNameLength)]
            public string Name { get; set; }
            [Required]
            public bool Hot { get; set; } = false;
            [Required]
            public int SortOrder { get; set; }
            [Required]
            public Status Status { get; set; }
        }
    }
}
