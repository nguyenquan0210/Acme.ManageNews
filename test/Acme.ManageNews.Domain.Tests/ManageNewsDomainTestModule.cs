using Acme.ManageNews.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.ManageNews;

[DependsOn(
    typeof(ManageNewsEntityFrameworkCoreTestModule)
    )]
public class ManageNewsDomainTestModule : AbpModule
{

}
