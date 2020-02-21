using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using Web.Identity.Configuration;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Web.Identity.Extensions
{
	public class ClientStore
		: IClientStore
	{
		private readonly IOptions<IdentityOptions> _options;

		public ClientStore(IOptions<IdentityOptions> options)
		{
			_options = options;
		}

		public async Task<Client> FindClientByIdAsync(string clientId)
		{
			// fetch client based on client id, including his valid secrets and/or certificates

			return new Client()
			{
				ClientId = clientId,
				AllowedScopes = Constants.Scopes.All,
				RequireClientSecret = true,
				ClientSecrets = null, // list of secrets and/or certificates
				AccessTokenLifetime = Convert.ToInt32(_options.Value.Token.Lifetime.TotalSeconds),
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
			};
		}
	}
}