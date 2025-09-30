using Microsoft.AspNetCore.Mvc;

namespace EbeeCleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    /// <summary>
    /// Retrieves a collection of weather forecasts for the next five days.
    /// </summary>
    /// <remarks>Each forecast includes the date, temperature in Celsius, and a summary description. The
    /// temperature is randomly generated within the range of -20 to 55 degrees Celsius,  and the summary is selected
    /// randomly from a predefined set of summaries.</remarks>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="WeatherForecast"/> objects representing the weather forecasts.</returns>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        logger.LogInformation("Getting weather forecast");

        IEnumerable<WeatherForecast> weatherForecasts = [.. Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            })];

        logger.LogInformation("Returning {Count} weather forecasts", weatherForecasts.Count());

        return weatherForecasts;
    }
}
