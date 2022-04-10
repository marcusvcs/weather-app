// Interfaces used in services: represent the API response

export interface LocationModel {
  city: string;
  state: string;
  country: string;
  key: number;
}

export interface CurrentWeatherModel {
  localObservationDateTime: string;
  weatherText: string;
  weatherIcon: number;
  temperatureC: number;
  temperatureF: number;
  relativeHumidity: number;
  windM: number;
  windI: number;

}

export interface DailyForecastModel {
  dailyForecasts: DailyForecast[];
}

export interface DailyForecast {
  date: string;
  temperature: TemperatureForecast;
  day: Day;
}

export interface TemperatureForecast {
  minimum: Minimum;
  maximum: Maximum;
}

export interface Maximum {
  value: number;
  unit: string;
}

export interface Minimum {
  value: number;
  unit: string;
}

export interface Day {
  icon: number;
  iconPhrase: string;

}
