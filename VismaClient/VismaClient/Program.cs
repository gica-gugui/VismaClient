using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using VismaClient.Handlers;
using VismaClient.Options;
using VismaClient.Services;
using VismaClient.Services.Interfaces;

namespace VismaClient
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureHostConfiguration(builder => {
                    builder.AddEnvironmentVariables();

                    if (args != null)
                    {
                        builder.AddCommandLine(args);
                    }
                })
                .ConfigureAppConfiguration((hostContext, builder) => {
                    var env = hostContext.HostingEnvironment;
                    builder.SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();

                    services.Configure<AppOptions>(hostContext.Configuration.GetSection("AppOptions"));
                    services.Configure<EnterpriseOptions>(hostContext.Configuration.GetSection("EnterpriseOptions"));
                    services.Configure<PartnerOptions>(hostContext.Configuration.GetSection("PartnerOptions"));

                    services.AddSingleton<IHostedService, ConsoleApplication>();

                    services.AddTransient<EnterpriseAuthorizationHeaderHandler>();

                    services.AddHttpClient<IDocumentService, DocumentService>((sp, c) =>
                    {
                        var appOptions = sp.GetService<IOptions<AppOptions>>().Value;

                        c.BaseAddress = new Uri(appOptions.BaseAddress);
                    })
                    .AddHttpMessageHandler<EnterpriseAuthorizationHeaderHandler>();

                    services.AddHttpClient<IInvitationService, InvitationService>((sp, c) =>
                    {
                        var appOptions = sp.GetService<IOptions<AppOptions>>().Value;

                        c.BaseAddress = new Uri(appOptions.BaseAddress);
                    });

                    services.AddHttpClient<IBearerService, BearerService>((sp, c) =>
                    {
                        var appOptions = sp.GetService<IOptions<AppOptions>>().Value;

                        c.BaseAddress = new Uri(appOptions.BaseAddress);
                    });

                    services.AddHttpClient<IOrganizationService, OrganizationService>((sp, c) =>
                    {
                        var appOptions = sp.GetService<IOptions<AppOptions>>().Value;

                        c.BaseAddress = new Uri(appOptions.BaseAddress);
                    })
                    .AddHttpMessageHandler<PartnerAuthorizationHeaderHandler>();
                });

            await builder.RunConsoleAsync();
        }
    }
}
