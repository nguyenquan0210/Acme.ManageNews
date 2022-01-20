using Acme.ManageNews.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Acme.ManageNews.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ManageNewsPageModel : AbpPageModel
{
    protected ManageNewsPageModel()
    {
        LocalizationResourceType = typeof(ManageNewsResource);
    }
}
