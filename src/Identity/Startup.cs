using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityModel;
using idunno.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Web.Identity.Configuration;
using Web.Identity.Extensions;

namespace Web.Identity
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			IdentityOptions identityOptions = Configuration.GetSection("Identity").Get<IdentityOptions>();

			services.Configure<IdentityOptions>(Configuration.GetSection("Identity"));
			X509Certificate2 cert = new EmbeddedCertificateProvider()
				.GetCertificateAsync().GetAwaiter().GetResult();

			services.AddIdentityServer(options =>
			{
				options.Endpoints.EnableAuthorizeEndpoint = false;
				options.Endpoints.EnableDeviceAuthorizationEndpoint = false;

				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseSuccessEvents = true;

				options.MutualTls.Enabled = true;
				options.MutualTls.ClientCertificateAuthenticationScheme = "x509";

				options.IssuerUri = identityOptions.Token.IssuerUri;
			})
				.AddClientStore<ClientStore>()
				.AddResourceStore<ResourceStore>()
				// TODO: replace with an implementation similar to https://github.com/damienbod/IdentityServer4AspNetCoreIdentityTemplate/issues/30 to enable azure key vault support
				.AddSigningCredential(cert)
				.AddValidationKey(cert)
				.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
				.AddMutualTlsSecretValidators();

			services.AddHttpContextAccessor();

			//services.AddTransient<ClaimsPrincipal>(c => ClaimsPrincipal.Current);

			services.AddAuthentication()
				.AddCertificate("x509", options =>
				{
					options.RevocationMode = X509RevocationMode.NoCheck;
					options.AllowedCertificateTypes = CertificateTypes.All;

					options.Events = new CertificateAuthenticationEvents
					{
						OnAuthenticationFailed = context =>
						{
							context.Fail("Certificate authentication failed.");
							return Task.CompletedTask;
						},
						OnValidateCertificate = context =>
						{
							context.Principal = Principal.CreateFromCertificate(context.ClientCertificate, includeAllClaims: true);
							context.Success();

							return Task.CompletedTask;
						}
					};
				});

			services.AddApplicationInsightsTelemetry();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			if (!env.IsDevelopment())
			{
				RewriteOptions rewriteOptions = new RewriteOptions()
					.AddRedirect(@"^\/?$", "/.well-known/openid-configuration");
				app.UseRewriter(rewriteOptions);
			}

			app.UseSerilogRequestLogging();

			app.UseIdentityServer();
		}
	}
}
