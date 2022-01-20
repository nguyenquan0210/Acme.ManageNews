using Acme.ManageNews.Catalog.Categories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Acme.ManageNews.Categories
{
    public class CategoryAppService_Tests : ManageNewsApplicationTestBase
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryAppService_Tests()
        {
            _categoryAppService = GetRequiredService<ICategoryAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Categorys_Without_Any_Filter()
        {
            var result = await _categoryAppService.GetListAsync(new GetCategoryListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(Category => Category.Name == "Xã Hội");
            result.Items.ShouldContain(Category => Category.Name == "Pháp Luật");
        }

        [Fact]
        public async Task Should_Get_Filtered_Categorys()
        {
            var result = await _categoryAppService.GetListAsync(
                new GetCategoryListDto { Filter = "Xã" });

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(Category => Category.Name == "Xã Hội");
            result.Items.ShouldNotContain(Category => Category.Name == "Pháp Luật");
        }

        [Fact]
        public async Task Should_Create_A_New_Category()
        {
            var CategoryDto = await _categoryAppService.CreateAsync(
                new CreateCategoryDto
                {
                    Name = "Giải Trí",
                    SortOrder = 1,
                    Status = Enums.Status.Active
                }
            );

            CategoryDto.Id.ShouldNotBe(Guid.Empty);
            CategoryDto.Name.ShouldBe("Giải Trí");
        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_Category()
        {
            await Assert.ThrowsAsync<CategoryAlreadyExistsException>(async () =>
            {
                await _categoryAppService.CreateAsync(
                    new CreateCategoryDto
                    {
                        Name = "Pháp Luật",
                        SortOrder = 1,
                        Status = Enums.Status.Active
                    }
                );
            });
        }

        //TODO: Test other methods...
    }
}
