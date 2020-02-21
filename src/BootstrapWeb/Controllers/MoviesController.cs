using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Messages;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace BootstrapWeb.Controllers
{
	public class MoviesController
		: ControllerBase
	{
		private readonly IMapper _mapper;

		public MoviesController(IMapper mapper)
		{
			_mapper = mapper;
		}

		[HttpGet]
		[EnableQuery]
		public IActionResult Get()
		{
			IQueryable<Movie> movies = new Movie[]
			{
				new Movie()
				{
					Id = 1,
					Title = "Movie 1"
				},
				new Movie()
				{
					Id = 2,
					Title = "Movie 2"
				}
			}.AsQueryable();

			return Ok(movies.ProjectTo<MovieDto>(_mapper.ConfigurationProvider));
		}
	}
}
