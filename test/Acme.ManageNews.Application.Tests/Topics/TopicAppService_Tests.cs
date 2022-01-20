using Acme.ManageNews.Catalog;
using Acme.ManageNews.Catalog.Topics;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Acme.ManageNews.Topics
{
    public class TopicAppService_Tests : ManageNewsApplicationTestBase
    {
        private readonly ITopicAppService _topicAppService;

        public TopicAppService_Tests()
        {
            _topicAppService = GetRequiredService<ITopicAppService>();
        }

        [Fact]
        public async Task Should_Get_All_topics_Without_Any_Filter()
        {
            var result = await _topicAppService.GetListAsync(new GetCatalogListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(Topic => Topic.Name == "Món ngon mỗi ngày");
            result.Items.ShouldContain(Topic => Topic.Name == "Thời tiết hôm nay");
        }

        [Fact]
        public async Task Should_Get_Filtered_topics()
        {
            var result = await _topicAppService.GetListAsync(
                new GetCatalogListDto { Filter = "Món" });

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(Topic => Topic.Name == "Món ngon mỗi ngày");
            result.Items.ShouldNotContain(Topic => Topic.Name == "Thời tiết hôm nay");
        }

        [Fact]
        public async Task Should_Create_A_New_topic()
        {
            var TopicDto = await _topicAppService.CreateAsync(
                new CreateTopicDto
                {
                    Name = "Tai nạn giao thông",
                    SortOrder = 1,
                    Status = Enums.Status.Active,
                    Hot = true
                }
            );

            TopicDto.Id.ShouldNotBe(Guid.Empty);
            TopicDto.Name.ShouldBe("Tai nạn giao thông");
        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_topic()
        {
            await Assert.ThrowsAsync<CatalogAlreadyExistsException>(async () =>
            {
                await _topicAppService.CreateAsync(
                    new CreateTopicDto
                    {
                        Name = "Thời tiết hôm nay",
                        SortOrder = 1,
                        Status = Enums.Status.Active,
                        Hot = true
                    }
                );
            });
        }

        //TODO: Test other methods...
    }
}
