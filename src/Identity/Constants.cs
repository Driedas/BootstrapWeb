using System;

namespace Web.Identity
{
	public static class Constants
	{
		public static class Token
		{
			private static readonly TimeSpan _defaultTokenLifetime = TimeSpan.FromMinutes(15);

			public static TimeSpan DefaultTokenLifetime { get { return _defaultTokenLifetime; } }
		}

		public static class Logging
		{
			public const string Template = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
		}

		public static class Scopes
		{
			public static string Foo { get { return "Foo";  } }

			public static string[] All { get { return new string[] { "openid", Foo, "bar" }; } }
		}

		public static class ClaimNames
		{
			public static string UserId { get { return "UserId"; } }
		}
	}
}