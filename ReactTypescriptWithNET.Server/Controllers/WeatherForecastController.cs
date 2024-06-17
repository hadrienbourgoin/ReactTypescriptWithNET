using Microsoft.AspNetCore.Mvc;
using ReactTypescriptWithNET.Server.Data;
using ReactTypescriptWithNET.Server.Models;

namespace ReactTypescriptWithNET.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ApplicationDbContext context, ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Forecast> Get()
        {
            return _context.Forecasts.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
        }
    }
}
