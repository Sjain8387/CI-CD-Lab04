using System.Reflection;
using LineTen.Core;
using MyFunctionApp.Functions;
using MyFunctionApp.Services.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]
namespace MyFunctionApp.Functions
{
    /// <inheritdoc />
    public class Startup :FunctionsStartup
    {
        /// <inheritdoc />
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Register your services here eg:
            //builder.Services.AddTransient<TInterface, TImplementation>(); //register a transient instance 
            //builder.Services.RegisterAllTypes<TInterface>(Assembly.GetAssembly(typeof(TImplementation)), ServiceLifetime.Transient); register all types 
            builder.Services.AddSingleton<ILoggingService, LoggingService>();
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();
        }
    }
}
