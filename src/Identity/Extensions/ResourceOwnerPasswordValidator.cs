using System;
using System.Security.Claims;
using System.Threading.Tasks;

using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;

namespace Web.Identity.Extensions
{
	public class ResourceOwnerPasswordValidator
		: IResourceOwnerPasswordValidator
	{
		private readonly ILogger<ResourceOwnerPasswordValidator> _log;

		public ResourceOwnerPasswordValidator(ILogger<ResourceOwnerPasswordValidator> log)
		{
			_log = log;
		}

		public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
		{
			// get user from the database based on context.UserName and context.Password
			int? userId = null;
			if (context.UserName == "foo")
			{
				userId = 10;
			}

			if (userId != null)
			{
				context.Result = new GrantValidationResult("subject name", "identity", new Claim[]
				{
					new Claim(Constants.ClaimNames.UserId, userId.ToString())
				});
			}
			else
			{
				context.Result.Error = $"User validation failed";
			}

			return Task.CompletedTask;
		}
	}
}