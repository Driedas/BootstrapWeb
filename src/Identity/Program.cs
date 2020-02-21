using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;

namespace Web.Identity
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				//.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				//.MinimumLevel.Override("System", LogEventLevel.Warning)
				//.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
				.Enrich.FromLogContext()
				.Enrich.WithExceptionDetails()
				.WriteTo.ApplicationInsights(new TraceTelemetryConverter())
				.WriteTo.Debug(outputTemplate: Constants.Logging.Template)
				.CreateLogger();

			CreateWebHostBuilder(args)
				.Build()
				.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureKestrel(options =>
				{
					options.AddServerHeader = false;
					options.ConfigureHttpsDefaults(opt =>
					{
						opt.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
						opt.ClientCertificateValidation = (cert, chain, errors) => true;
					});
				})
				.UseSerilog();
		}
	}
}
