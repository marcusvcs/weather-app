namespace WeatherBackend.Model
{

    #region Classes generated from Copy JSON as Class
    public class DailyForecastModel
    {
        public Dailyforecast[]? DailyForecasts { get; set; }
    }

    public class Dailyforecast
    {
        public DateTime Date { get; set; }
        public TemperatureForecast? Temperature { get; set; }
        public Day? Day { get; set; }
        
    }

    public class TemperatureForecast
    {
        public Minimum? Minimum { get; set; }
        public Maximum? Maximum { get; set; }
    }

    public class Minimum
    {
        public float Value { get; set; }
        public string? Unit { get; set; }
    }

    public class Maximum
    {
        public float Value { get; set; }
        public string? Unit { get; set; }
    }

    public class Day
    {
        public int Icon { get; set; }
        public string? IconPhrase { get; set; }
    }
    #endregion


}
