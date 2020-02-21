using System;

using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Web.Identity.Extensions
{
	public class ResourceStore
		: InMemoryResourcesStore
	{
		private static readonly IdentityResource[] _identityResources = new IdentityResource[] { new IdentityResources.OpenId() };
		private static readonly ApiResource[] _apiResources = new ApiResource[]
		{
			new ApiResource(Constants.Scopes.Foo)
			{
				UserClaims = new string[]{ Constants.ClaimNames.UserId },
			},
		};

		public ResourceStore()
			: base(identityResources: _identityResources, apiResources: _apiResources)
		{
		}
	}
}
