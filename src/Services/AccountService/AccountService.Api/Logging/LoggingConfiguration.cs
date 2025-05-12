using Serilog;

namespace AccountService.Api.Logging;

public static class LoggingConfiguration
{
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }
}
