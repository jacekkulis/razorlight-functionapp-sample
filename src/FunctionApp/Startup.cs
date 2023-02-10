using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

[assembly: FunctionsStartup(typeof(Startup))]
namespace RazorLight
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            builder.Services.AddFunctionAppServices(configuration);
        }
    }
}

