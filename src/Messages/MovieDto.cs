﻿using System;

namespace Messages
{
	public class MovieDto
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public TimeSpan Duration { get; set; }
	}
}