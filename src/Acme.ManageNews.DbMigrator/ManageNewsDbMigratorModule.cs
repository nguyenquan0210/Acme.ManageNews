using Acme.ManageNews.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Acme.ManageNews.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ManageNewsEntityFrameworkCoreModule),
    typeof(ManageNewsApplicationContractsModule)
    )]
public class ManageNewsDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
