namespace TodoList.Endpoints;

public static class SampleEndpoints
{
  public static void RegisterSampleEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var summaries = new[]
    {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    endpoints.MapGet("/weatherforecast", () =>
      {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
              DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
              Random.Shared.Next(-20, 55),
              summaries[Random.Shared.Next(summaries.Length)]
            ))
          .ToArray();
        return forecast;
      })
      .WithName("GetWeatherForecast")
      .WithOpenApi();

  }

}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
