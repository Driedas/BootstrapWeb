using System;

using AutoMapper;
using BootstrapWeb.Configuration.Automapper;
using Microsoft.Extensions.DependencyInjection;

namespace BootstrapWeb.Configuration
{
	public static class AutomapperExtensions
	{
		public static void AddCustomAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MovieProfile).Assembly);
		}
	}
}
