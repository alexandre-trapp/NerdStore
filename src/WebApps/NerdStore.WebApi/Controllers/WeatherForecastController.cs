using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NerdStore.Catalogo.Domain.Services;

namespace NerdStore.WebApi.Controllers
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
        private readonly IEstoqueService _estoqueService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IEstoqueService estoqueService)
        {
            _logger = logger;
            _estoqueService = estoqueService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _estoqueService.DebitarEstoque(new Guid("6cf914d3-7dd6-49b0-8dca-ec799abe6b40"), 1);

            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
