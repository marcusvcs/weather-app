using WeatherBackend;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy => {
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

//Will be configured outside the application
//app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseFileServer();


app.MapGet("/currentweather", async (string locationKey, IWeatherService weatherService) => {

    var currentWeather = await weatherService.GetCurrentWeather(locationKey);

    return currentWeather;
})
.WithName("GetCurrentWeather");

app.MapGet("/location", async (string latitude, string longitude, IWeatherService weatherService) => {
    var location = await weatherService.GetLocation(latitude, longitude);

    return location;
})
.WithName("GetCurrentLocation");

app.MapGet("/dailyforecast", async (string locationKey, IWeatherService weatherService) => {

    var dailyForecast = await weatherService.GetDailyForecast(locationKey);

    return dailyForecast;
})
.WithName("GetDailyForecast");

app.Run();
