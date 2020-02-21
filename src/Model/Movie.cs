using System;

namespace Model
{
	public class Movie
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public TimeSpan Duration { get; set; }

		public string Synopsis { get; set; }
	}
}
