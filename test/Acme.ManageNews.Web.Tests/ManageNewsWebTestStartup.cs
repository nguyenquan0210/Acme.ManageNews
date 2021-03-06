using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Acme.ManageNews;

public class ManageNewsWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ManageNewsWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
