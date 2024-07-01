using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace Common.Logs.Serilog
{
    public class ApplicaitonEnricher : ILogEventEnricher
    {
        private readonly SerilogApplicationEnricherOptions _options;
        public ApplicaitonEnricher(IOptions<SerilogApplicationEnricherOptions> options)
        {
            _options = options.Value;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_options.ApplicationName), _options.ApplicationName));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_options.ApplicationInstanceId), _options.ApplicationInstanceId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(_options.ApplicationStartDate), _options.ApplicationStartDate));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(nameof(Environment.MachineName), Environment.MachineName));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("EntryPoint", Assembly.GetEntryAssembly().GetName().Name));
        }
    }
}
