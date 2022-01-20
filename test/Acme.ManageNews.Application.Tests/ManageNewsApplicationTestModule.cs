using Volo.Abp.Modularity;

namespace Acme.ManageNews;

[DependsOn(
    typeof(ManageNewsApplicationModule),
    typeof(ManageNewsDomainTestModule)
    )]
public class ManageNewsApplicationTestModule : AbpModule
{

}
