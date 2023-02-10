using Microsoft.Extensions.Configuration;
using PostmarkDotNet;
using RazorLight;
using RazorLight.Extensions;
using RazorLight.Samples;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddFunctionAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorLight("RazorLight.Samples.Views");
        services.AddPostmarkClient(configuration);
        return services;
    }

    public static IServiceCollection AddRazorLight(this IServiceCollection services, string rootNamespace)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        RazorLightOptions opts = new RazorLightOptions()
        {
            EnableDebugMode = true,
        };

        services.AddRazorLight(() => new RazorLightEngineBuilder()
         .UseEmbeddedResourcesProject(executingAssembly, rootNamespace)
         .SetOperatingAssembly(executingAssembly)
         .UseMemoryCachingProvider()
         .Build());

        return services;
    }

    public static IServiceCollection AddPostmarkClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PostmarkSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(PostmarkSettings)).Bind(settings);
            });

        var postmarkSettings = configuration.GetSection(nameof(PostmarkSettings)).Get<PostmarkSettings>();
        services.AddSingleton<PostmarkClient>(x => new PostmarkClient(postmarkSettings.ServerToken));
        return services;
    }
}