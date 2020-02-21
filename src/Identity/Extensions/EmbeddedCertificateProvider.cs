using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Web.Identity.Extensions
{
	public class EmbeddedCertificateProvider
		: ICertificateProvider
	{
		private readonly static X509Certificate2 _certificate;

		static EmbeddedCertificateProvider()
		{
			using (Stream certStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{typeof(Startup).Namespace}.Resources.Identity.pfx"))
			{
				byte[] certBytes = new byte[certStream.Length];
				certStream.Read(certBytes, 0, certBytes.Length);
				_certificate = new X509Certificate2(certBytes, "5656c202-6bfa-4794-bbc0-99cc3d2b9a8a", X509KeyStorageFlags.MachineKeySet);
			}
		}

		public Task<X509Certificate2> GetCertificateAsync()
		{
			return Task.FromResult(_certificate);
		}
	}
}