using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Acme.ManageNews.Web;

[Dependency(ReplaceServices = true)]
public class ManageNewsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ManageNews";
}
