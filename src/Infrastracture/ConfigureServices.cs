using Microsoft.Extensions.Configuration;
using NotificationProcessor.Infrastructure.Configurations;
using PostmarkDotNet;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostmarkClient(configuration);
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
