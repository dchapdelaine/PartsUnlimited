using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.Owin;
using Owin;
using PartsUnlimited;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(Startup))]

//comment
namespace PartsUnlimited
{
    // bellevue comment!!
    // second commit
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var configuration = TelemetryConfiguration.Active;

            configuration.InstrumentationKey = WebConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];

            QuickPulseTelemetryProcessor processor = null;

            configuration.TelemetryProcessorChainBuilder
                .Use((next) =>
                {
                    processor = new QuickPulseTelemetryProcessor(next);
                    return processor;
                })
                        .Build();

            var QuickPulse = new QuickPulseTelemetryModule()
            {
                AuthenticationApiKey = WebConfigurationManager.AppSettings["APPINSIGHTS_APIKEY"]
            };
            QuickPulse.Initialize(configuration);
            QuickPulse.RegisterTelemetryProcessor(processor);
            foreach (var telemetryProcessor in configuration.TelemetryProcessors)
            {
                if (telemetryProcessor is ITelemetryModule telemetryModule)
                {
                    telemetryModule.Initialize(configuration);
                }
            }
        }
    }
}