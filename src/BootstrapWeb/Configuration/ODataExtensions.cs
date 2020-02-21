using System;
using Messages;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Routing;

namespace BootstrapWeb.Configuration
{
	public static class ODataExtensions
	{
		public static IRouteBuilder AddOData(this IRouteBuilder builder)
		{
			var modelBuilder = new ODataConventionModelBuilder();
			modelBuilder
				.AddMovies("Movies");

			builder.MapODataServiceRoute("odata", "odata", modelBuilder.GetEdmModel());

			// apply common query restrictions
			builder.MaxTop(10);
			builder.OrderBy(QueryOptionSetting.Disabled);
			builder.Expand(QueryOptionSetting.Disabled);
			// ...

			return builder;
		}

		private static ODataModelBuilder AddMovies(this ODataModelBuilder builder, string routeBase)
		{
			builder.EntitySet<MovieDto>(routeBase);

			return builder;
		}
	}
}