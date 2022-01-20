using System;
using System.Collections.Generic;
using System.Text;
using Acme.ManageNews.Localization;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews;

/* Inherit your application services from this class.
 */
public abstract class ManageNewsAppService : ApplicationService
{
    protected ManageNewsAppService()
    {
        LocalizationResource = typeof(ManageNewsResource);
    }
}
