using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				_logger.LogInformation("Hey, this is a request!");

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
				_logger.LogError(e, "Something bad happened.");

				return new StatusCodeResult(500);
			}
		}
	}
}
