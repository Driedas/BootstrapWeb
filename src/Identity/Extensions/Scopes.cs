using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace Web.Identity.Extensions
{
	public class Scopes
	{
		public static IEnumerable<ApiScope> SupportedScopes
		{
			get
			{

				yield return new ApiScope(OidcConstants.StandardScopes.OpenId);
				yield return new ApiScope(Constants.Scopes.Foo, "Foo API");
			}
		}
	}
}
