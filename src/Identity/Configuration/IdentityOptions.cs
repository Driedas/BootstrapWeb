using System;

namespace Web.Identity.Configuration
{
	public class IdentityOptions
	{
		public IdentityTokenOptions Token { get; set; }
	}

	public class IdentityTokenOptions
	{
		public TimeSpan Lifetime { get; set; } = Constants.Token.DefaultTokenLifetime;

		public string IssuerUri { get; set; }
	}
}