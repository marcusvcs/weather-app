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

  constructor(private readonly geolocation$: GeolocationService, private weatherService: WeatherService) {

  }
  ngOnInit() {
    /*
    this.geolocation$.pipe(take(1)).subscribe(position => {

      this.weatherService.getLocation(position.coords.latitude, position.coords.longitude).subscribe(loc => {
        this.locationModel = loc;
        this.weatherService.getCurrentWeather(this.locationModel.key).subscribe(currentWeather => {
          this.currentWeatherModel = currentWeather;
          this.weatherService.getDailyForecast(this.locationModel.key).subscribe(dailyForecast => {
            this.dailyForecastModel = dailyForecast;
          });
        });

      });
    });
    */

  }
  title = 'weatherapp';

}

