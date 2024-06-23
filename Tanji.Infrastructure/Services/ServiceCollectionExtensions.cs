using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using Tanji.Core.Net;
using Tanji.Core.Net.Interception;
using Tanji.Infrastructure.Factories;
using Tanji.Infrastructure.ViewModels;
using Tanji.Infrastructure.Configuration;
using Tanji.Infrastructure.Services.Implementations;
using Tanji.Infrastructure.Factories.Implementations;

namespace Tanji.Infrastructure.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTanjiCore(this IServiceCollection services)
    {
        // Add configuration
        services.AddOptions();
        services.AddSingleton<IPostConfigureOptions<TanjiOptions>, PostConfigureTanjiOptions>();

        // Factories
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();

        // Singleton Services
        services.AddSingleton<IMiddleman, FlashPacketMiddlemanService>();
        services.AddSingleton<IClientHandlerService, ClientHandlerService>();
        services.AddSingleton<IWebInterceptionService, EavesdropInterceptionService>();
        services.AddSingleton<IRemoteEndPointResolverService<HotelEndPoint>, RemoteHotelEndPointResolverService>();
        services.AddSingleton<IConnectionHandlerService, ConnectionHandlerService>();

        // View Models
        services.AddSingleton<ConnectionViewModel>();
        services.AddSingleton<InjectionViewModel>();
        services.AddSingleton<ToolboxViewModel>();
        services.AddSingleton<ExtensionsViewModel>();
        services.AddSingleton<SettingsViewModel>();

        return services;
    }
}