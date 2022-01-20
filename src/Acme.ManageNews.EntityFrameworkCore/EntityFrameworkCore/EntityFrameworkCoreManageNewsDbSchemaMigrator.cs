using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.ManageNews.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.ManageNews.EntityFrameworkCore;

public class EntityFrameworkCoreManageNewsDbSchemaMigrator
    : IManageNewsDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreManageNewsDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ManageNewsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ManageNewsDbContext>()
            .Database
            .MigrateAsync();
    }
}
