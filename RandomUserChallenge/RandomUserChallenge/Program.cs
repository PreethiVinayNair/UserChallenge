using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UserChallenge
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      try
      {
        CreateWebHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Stopped program because of exception");
        throw;
      }
      finally
      {
        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        LogManager.Shutdown();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseSetting("detailedErrors", "true")
            .UseStartup<Startup>()
            .CaptureStartupErrors(true)
            .ConfigureLogging(logging =>
            {
              logging.ClearProviders();
              // Logging configuration specified in appsettings.json overrides any call to SetMinimumLevel
              logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseNLog();
  }
}
