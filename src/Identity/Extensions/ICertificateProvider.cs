using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Web.Identity.Extensions
{
	public interface ICertificateProvider
	{
		Task<X509Certificate2> GetCertificateAsync();
	}
}
