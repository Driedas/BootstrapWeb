using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootstrapWeb.Controllers
{
	[Route("Foo")]
	[Authorize]
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