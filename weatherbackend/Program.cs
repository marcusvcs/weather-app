using WeatherBackend;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:4200"
                                              );
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);


app.MapGet("/currentweather", async (string locationKey, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var weatherService = new WeatherService(client);
    var currentWeather = await weatherService.GetCurrentWeather(locationKey);

    return currentWeather;
})
.WithName("GetCurrentWeather");

app.MapGet("/location", async (string latitude, string longitude, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var weatherService = new WeatherService(client);
    var currentWeather = await weatherService.GetLocation(latitude, longitude);

    return currentWeather;
})
.WithName("GetCurrentLocation");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}