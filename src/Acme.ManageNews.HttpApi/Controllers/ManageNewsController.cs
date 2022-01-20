using Acme.ManageNews.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.ManageNews.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ManageNewsController : AbpControllerBase
{
    protected ManageNewsController()
    {
        LocalizationResource = typeof(ManageNewsResource);
    }
}
