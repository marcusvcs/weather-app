using Newtonsoft.Json;
using System.Text.Json;
using weatherbackend.Model;
using WeatherBackend.Model;

namespace WeatherBackend
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;


        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LocationModel?> GetLocation(string latitude, string longitude)
        {

            _httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/locations/v1/cities/geoposition/search");
            string urlParameters = $"?q={latitude},{longitude}&apikey={Environment.GetEnvironmentVariable("WEATHER_API_KEY")}";

            var response = await _httpClient.GetAsync(urlParameters);
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

        public async Task<CurrentWeatherModel?> GetCurrentWeather(string locationKey)
        {

            _httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/currentconditions/v1/");
            string urlParameters = $"{locationKey}?apikey={Environment.GetEnvironmentVariable("WEATHER_API_KEY")}&details=true";

            var response = await _httpClient.GetAsync(urlParameters);
            JsonSerializerOptions serializerOptions = new();
            serializerOptions.IncludeFields = true;
            if (response != null && response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<List<CurrentWeather>>(serializerOptions);
                if (results != null)
                {
                    var fullResult =  results.FirstOrDefault();
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

        public async Task<DailyForecastModel?> GetDailyForecast(string locationKey)
        {

            _httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/forecasts/v1/daily/5day/");
            string urlParameters = $"{locationKey}?apikey={Environment.GetEnvironmentVariable("WEATHER_API_KEY")}";

            var response = await _httpClient.GetAsync(urlParameters);
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
