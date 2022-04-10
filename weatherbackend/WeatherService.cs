using System.Text.Json;
using WeatherBackend.Model;

namespace WeatherBackend
{
    /// <summary>
    /// Class that gets all the weather data from the AccuWeather API
    /// </summary>
    public class WeatherService : IWeatherService
    {
        // Injected by DI
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        /// <summary>
        /// Get location data from openweathermap.org
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <returns></returns>
        public async Task<LocationModel?> GetLocation(string latitude, string longitude)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/locations/v1/cities/geoposition/search");
            string urlParameters = $"?q={latitude},{longitude}&apikey={Environment.GetEnvironmentVariable("WEATHER_API_KEY")}";

            var response = await httpClient.GetAsync(urlParameters);
            JsonSerializerOptions serializerOptions = new();
            serializerOptions.IncludeFields = true;
            if (response != null && response.IsSuccessStatusCode)
            {
                var fullResponse = await response.Content.ReadFromJsonAsync<Location>(serializerOptions);
                if (fullResponse != null)
                {
                    return new LocationModel()
                    {
                        City = fullResponse.LocalizedName,
                        Country = fullResponse.Country.LocalizedName,
                        State = fullResponse.AdministrativeArea.LocalizedName,
                        Key = fullResponse.Key
                    };
                }

            }
            return null;

        }
        
        /// <summary>
        /// Gets the weather for a specific location
        /// </summary>
        /// <param name="locationKey">The location key that is received in the GetLocation method</param>
        /// <returns></returns>
        public async Task<CurrentWeatherModel?> GetCurrentWeather(string locationKey)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/currentconditions/v1/");
            string urlParameters = $"{locationKey}?apikey={Environment.GetEnvironmentVariable("WEATHER_API_KEY")}&details=true";

            var response = await httpClient.GetAsync(urlParameters);
            JsonSerializerOptions serializerOptions = new();
            serializerOptions.IncludeFields = true;
            if (response != null && response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<List<CurrentWeather>>(serializerOptions);
                if (results != null)
                {
                    var fullResult = results.FirstOrDefault();
                    if (fullResult != null)
                    {
                        return new CurrentWeatherModel()
                        {
                            LocalObservationDateTime = fullResult.LocalObservationDateTime,
                            TemperatureC = fullResult.Temperature.Metric.Value,
                            TemperatureF = fullResult.Temperature.Imperial.Value,
                            WeatherText = fullResult.WeatherText,
                            WeatherIcon = fullResult.WeatherIcon,
                            RelativeHumidity = fullResult.RelativeHumidity,
                            WindM = fullResult.Wind.Speed.Metric.Value,
                            WindI = fullResult.Wind.Speed.Imperial.Value
                        };
                    }
                }
            }
            return null;

        }

        /// <summary>
        /// Get the forecast for the next 5 days
        /// </summary>
        /// <param name="locationKey">The location key that is received in the GetLocation method</param>
        /// <returns></returns>
        public async Task<DailyForecastModel?> GetDailyForecast(string locationKey)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/forecasts/v1/daily/5day/");
            string urlParameters = $"{locationKey}?apikey={Environment.GetEnvironmentVariable("WEATHER_API_KEY")}";

            var response = await httpClient.GetAsync(urlParameters);
            JsonSerializerOptions serializerOptions = new();
            serializerOptions.IncludeFields = true;
            if (response != null && response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DailyForecastModel>(serializerOptions);
                if (result != null)
                {
                    return result;
                }
            }
            return null;

        }



    }
}
