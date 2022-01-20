using Acme.ManageNews.Catalog.Topics;
using Acme.ManageNews.Catalogs;
using Acme.ManageNews.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.ManageNews.Web.Pages.Topics
{
    public class CreateModalModel : ManageNewsPageModel
    {
        [BindProperty]
        public CreateTopicViewModel Topic { get; set; }

        private readonly ITopicAppService _topicAppService;

        public CreateModalModel(ITopicAppService TopicAppService)
        {
            _topicAppService = TopicAppService;
        }

        public void OnGet()
        {
            Topic = new CreateTopicViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateTopicViewModel, CreateTopicDto>(Topic);
            await _topicAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateTopicViewModel
        {
            [Required]
            [StringLength(CatalogConsts.MaxNameLength)]
            public string Name { get; set; }
           
            [Required]
            public int SortOrder { get; set; } = 1;
           
            [Required]
            public bool Hot { get; set; } = false;

        }
    }
}
