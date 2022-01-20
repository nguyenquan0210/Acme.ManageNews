using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Acme.ManageNews.Pages;

public class Index_Tests : ManageNewsWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
