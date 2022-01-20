using Acme.ManageNews.Catalog;
using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Eventss;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Acme.ManageNews.Eventss
{
    public class EventsAppService_Tests : ManageNewsApplicationTestBase
    {
        private readonly IEventsAppService _eventsAppService;
        private readonly ICategoryAppService _categoryAppService;

        public EventsAppService_Tests()
        {
            _eventsAppService = GetRequiredService<IEventsAppService>();
            _categoryAppService = GetRequiredService<ICategoryAppService>();
        }

        [Fact]
        public async Task Should_Get_All_eventss_Without_Any_Filter()
        {
            var result = await _eventsAppService.GetListAsync(new GetCatalogListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(Events => Events.Name == "Tiêm vaccine Covid-19 cho trẻ em" && Events.CategoryName =="Xã Hội");
            result.Items.ShouldContain(Events => Events.Name == "Loạt ca nhiễm nCoV mới ở Việt Nam" && Events.CategoryName == "Pháp Luật");
        }

        [Fact]
        public async Task Should_Get_Filtered_eventss()
        {
            var result = await _eventsAppService.GetListAsync(
                new GetCatalogListDto { Filter = "Tiêm" });

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(Events => Events.Name == "Tiêm vaccine Covid-19 cho trẻ em");
            result.Items.ShouldNotContain(Events => Events.Name == "Loạt ca nhiễm nCoV mới ở Việt Nam");
        }

        [Fact]
        public async Task Should_Create_A_New_events()
        {
            var authors = await _categoryAppService.GetListAsync(new GetCatalogListDto());
            var firstAuthor = authors.Items.First();

            var EventsDto = await _eventsAppService.CreateAsync(
                new CreateEventsDto
                {
                    Name = "Ô nhiễm không khí",
                    SortOrder = 1,
                    Status = Enums.Status.Active,
                    CategoryId = firstAuthor.Id,
                    Hot = false
                }
            );

            EventsDto.Id.ShouldNotBe(Guid.Empty);
            EventsDto.Name.ShouldBe("Ô nhiễm không khí");
        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_events()
        {
            await Assert.ThrowsAsync<CatalogAlreadyExistsException>(async () =>
            {
                await _eventsAppService.CreateAsync(
                    new CreateEventsDto
                    {
                        Name = "Loạt ca nhiễm nCoV mới ở Việt Nam",
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
