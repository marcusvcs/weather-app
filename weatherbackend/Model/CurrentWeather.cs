﻿namespace WeatherBackend.Model
{

    public class CurrentWeather
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public int RelativeHumidity { get; set; }
        public int IndoorRelativeHumidity { get; set; }

        public Wind Wind { get; set; }

    }

    public class Temperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class Metric
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Wind
    {
        public Direction Direction { get; set; }
        public Speed Speed { get; set; }
    }

    public class Direction
    {
        public int Degrees { get; set; }
        public string Localized { get; set; }
        public string English { get; set; }
    }

    public class Speed
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class CurrentWeatherModel
    {
        public DateTime LocalObservationDateTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public float TemperatureC { get; set; }
        public float TemperatureF { get; set; }
        public float RelativeHumidity { get; set; }
        public float WindM { get; set; }
        public float WindI { get; set; }
    }



}