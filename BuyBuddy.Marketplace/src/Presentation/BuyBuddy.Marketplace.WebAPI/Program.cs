using Serilog;

namespace BuyBuddy.Marketplace.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1️ Serilog configuration...
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Console()
               .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
               .Enrich.WithMachineName()
               .Enrich.WithThreadId()
               .Enrich.WithProcessId()
               .Enrich.WithEnvironmentName()
               .CreateLogger();

            try
            {
                Log.Information("Starting up the application...");

                var builder = WebApplication.CreateBuilder(args);

                // 2 Integrates Serilog into the host system...
                builder.Host.UseSerilog();

                // 3️ Call Startup...
                var startup = new Startup(builder.Configuration);
                startup.ConfigureServices(builder.Services);

                var app = builder.Build();
                startup.Configure(app, app.Environment);

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
