using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Configuration;

namespace SerilogLearning2Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger _logger;

		public WeatherForecastController(ILogger logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				_logger.Information("Hey, this is a request!");

				var rng = new Random();

				if (rng.Next(0, 5) > 2)
				{
					throw new Exception("There is an error occured!");
				}

				return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
					{
						Date = DateTime.Now.AddDays(index),
						TemperatureC = rng.Next(-20, 55),
						Summary = Summaries[rng.Next(Summaries.Length)]
					})
					.ToArray());
			}
			catch (Exception e)
			{
				_logger.Error(e, "Something bad happened {CustomProperty}.", 50); //add random custom property to then filter on it in elastic

				return new StatusCodeResult(500);
			}
		}
	}
}
