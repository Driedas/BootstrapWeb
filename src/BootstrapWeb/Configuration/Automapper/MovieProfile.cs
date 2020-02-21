using AutoMapper;
using Messages;
using Model;

namespace BootstrapWeb.Configuration.Automapper
{
	public class MovieProfile
		: Profile
	{
		public MovieProfile()
		{
			CreateMap<Movie, MovieDto>();
		}
	}
}