import { Component, OnInit } from '@angular/core';
import { GeolocationService } from '@ng-web-apis/geolocation';
import { take } from 'rxjs/operators';
import { LocationModel, CurrentWeatherModel, DailyForecastModel } from './weather.model'
import { WeatherService } from './weather.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  public locationModel: LocationModel = { city: 'Brasilia', country: 'Brazil', key: 0, state: 'DF' } as LocationModel
  public currentWeatherModel: CurrentWeatherModel = { "localObservationDateTime": "2022-04-08T15:35:00-03:00", "weatherText": "Clouds and sun", "weatherIcon": 4, "temperatureC": 30.3, "temperatureF": 87, "relativeHumidity": 45, "windM": 10, "windI": 6.2 } as CurrentWeatherModel;
  public dailyForecastModel: DailyForecastModel = { "dailyForecasts": [{ "date": "2022-04-08T07:00:00-03:00", "temperature": { "minimum": { "value": 70, "unit": "F" }, "maximum": { "value": 87, "unit": "F" } }, "day": { "icon": 3, "iconPhrase": "Partly sunny" } }, { "date": "2022-04-09T07:00:00-03:00", "temperature": { "minimum": { "value": 72, "unit": "F" }, "maximum": { "value": 91, "unit": "F" } }, "day": { "icon": 2, "iconPhrase": "Mostly sunny" } }, { "date": "2022-04-10T07:00:00-03:00", "temperature": { "minimum": { "value": 70, "unit": "F" }, "maximum": { "value": 91, "unit": "F" } }, "day": { "icon": 2, "iconPhrase": "Mostly sunny" } }, { "date": "2022-04-11T07:00:00-03:00", "temperature": { "minimum": { "value": 71, "unit": "F" }, "maximum": { "value": 92, "unit": "F" } }, "day": { "icon": 4, "iconPhrase": "Intermittent clouds" } }, { "date": "2022-04-12T07:00:00-03:00", "temperature": { "minimum": { "value": 71, "unit": "F" }, "maximum": { "value": 89, "unit": "F" } }, "day": { "icon": 4, "iconPhrase": "Intermittent clouds" } }] } as DailyForecastModel;
  public metricOrImperial: string | null = "";
  public windSpeedUnit: string = "";
  public windSpeed: number = 0;
  public temperature: number = 0;
  public temperatureUnit: string = "";

  constructor(private readonly geolocation$: GeolocationService, private weatherService: WeatherService) {

  }
  ngOnInit() {
    this.metricOrImperial = localStorage.getItem('metricOrImperial');
    if (this.metricOrImperial == null) {
      localStorage.setItem('metricOrImperial', "metric");
      this.metricOrImperial = "metric";
    }

    
    this.geolocation$.pipe(take(1)).subscribe(position => {

      this.weatherService.getLocation(position.coords.latitude, position.coords.longitude).subscribe(loc => {
        this.locationModel = loc;
        this.weatherService.getCurrentWeather(this.locationModel.key).subscribe(currentWeather => {
          this.currentWeatherModel = currentWeather;
          this.weatherService.getDailyForecast(this.locationModel.key).subscribe(dailyForecast => {
            this.dailyForecastModel = dailyForecast;
            this.setUnits();
            this.convertUnits(true);
          });
        });

      });
    });
    

    //this.setUnits();
    //this.convertUnits(true);

  }
  title = 'Weather on your City';

  public changeToCelsius() {
    let previousMetricOrImperial = localStorage.getItem('metricOrImperial');
    if (previousMetricOrImperial == "imperial") {
      localStorage.setItem('metricOrImperial', "metric");
      this.metricOrImperial = "metric";
      this.setUnits();
      this.convertUnits();
    }
  }

  public changeToFahrenheit() {
    let previousMetricOrImperial = localStorage.getItem('metricOrImperial');
    if (previousMetricOrImperial == "metric") {
      localStorage.setItem('metricOrImperial', "imperial");
      this.metricOrImperial = "imperial";
      this.setUnits();
      this.convertUnits();
    }
  }

  public setUnits() {
    if (this.metricOrImperial == "metric") {
      this.windSpeedUnit = "Km/h";
      this.windSpeed = this.currentWeatherModel.windM;
      this.temperature = this.currentWeatherModel.temperatureC;
      this.temperatureUnit = "C";
    } else {
      this.windSpeedUnit = "Mph";
      this.windSpeed = this.currentWeatherModel.windI;
      this.temperature = this.currentWeatherModel.temperatureF;
      this.temperatureUnit = "F";
    }
  }

  public convertUnits(firstTime: boolean = false) {
    if (this.metricOrImperial == "metric") {
      this.dailyForecastModel.dailyForecasts.map(temp => {
        temp.temperature.minimum.value = this.convertFahrenheitToCelsius(temp.temperature.minimum.value);
        temp.temperature.maximum.value = this.convertFahrenheitToCelsius(temp.temperature.maximum.value);
      });
    } else {
      if (!firstTime) {
        this.dailyForecastModel.dailyForecasts.map(temp => {
          temp.temperature.minimum.value = this.convertCelsiusToFahrenheit(temp.temperature.minimum.value);
          temp.temperature.maximum.value = this.convertCelsiusToFahrenheit(temp.temperature.maximum.value);
        });
      }
    }

  }

  //convert farhenheit to celsius
  public convertFahrenheitToCelsius(fahrenheit: number) {
    return Math.ceil((fahrenheit - 32) * 5 / 9);
  }
  //convert celsius to farhenheit
  public convertCelsiusToFahrenheit(celsius: number) {
    return Math.ceil(celsius * 9 / 5 + 32);
  }

}

