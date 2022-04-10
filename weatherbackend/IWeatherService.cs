using WeatherBackend.Model;

namespace WeatherBackend
{
    /// <summary>
    /// Interface needed to DI the WeatherService
    /// </summary>
    public interface IWeatherService
    {
        Task<CurrentWeatherModel?> GetCurrentWeather(string locationKey);
        Task<DailyForecastModel?> GetDailyForecast(string locationKey);
        Task<LocationModel?> GetLocation(string latitude, string longitude);
    }
}