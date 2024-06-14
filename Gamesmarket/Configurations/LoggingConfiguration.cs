using NLog.Web;

namespace Gamesmarket.Configurations
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();
        }
    }
}
