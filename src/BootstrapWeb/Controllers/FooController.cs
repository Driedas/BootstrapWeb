using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapWeb.Controllers
{
	[Route("Foo")]
	public class FooController
		: Controller
	{
		[HttpGet("")]
		public IActionResult Get()
		{
			return Ok();
		}

		[HttpPost]
		public IActionResult Post()
		{
			return Ok();
		}
	}
}