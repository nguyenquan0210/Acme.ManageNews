using Acme.ManageNews.Catalog.Cities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Acme.ManageNews.Cities
{
    public class CityAppService_Tests : ManageNewsApplicationTestBase
    {
        private readonly ICityAppService _cityAppService;

        public CityAppService_Tests()
        {
            _cityAppService = GetRequiredService<ICityAppService>();
        }

        [Fact]
        public async Task Should_Get_All_citys_Without_Any_Filter()
        {
            var result = await _cityAppService.GetListAsync(new GetCityListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(city => city.Name == "Hà Nội");
            result.Items.ShouldContain(city => city.Name == "Đà Nẵng");
        }

        [Fact]
        public async Task Should_Get_Filtered_citys()
        {
            var result = await _cityAppService.GetListAsync(
                new GetCityListDto { Filter = "Hà" });

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(city => city.Name == "Hà Nội");
            result.Items.ShouldNotContain(city => city.Name == "Đà Nẵng");
        }

        [Fact]
        public async Task Should_Create_A_New_city()
        {
            var cityDto = await _cityAppService.CreateAsync(
                new CreateCityDto
                {
                    Name = "TP HCM",
                    SortOrder = 1,
                    Status = Enums.Status.Active
                }
            );

            cityDto.Id.ShouldNotBe(Guid.Empty);
            cityDto.Name.ShouldBe("TP HCM");
        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_city()
        {
            await Assert.ThrowsAsync<CityAlreadyExistsException>(async () =>
            {
                await _cityAppService.CreateAsync(
                    new CreateCityDto
                    {
                        Name = "Đà Nẵng",
                        SortOrder = 1,
                        Status = Enums.Status.Active
                    }
                );
            });
        }

        //TODO: Test other methods...
    }
}
