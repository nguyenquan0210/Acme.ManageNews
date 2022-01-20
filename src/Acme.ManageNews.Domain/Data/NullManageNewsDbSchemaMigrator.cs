using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.ManageNews.Data;

/* This is used if database provider does't define
 * IManageNewsDbSchemaMigrator implementation.
 */
public class NullManageNewsDbSchemaMigrator : IManageNewsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
