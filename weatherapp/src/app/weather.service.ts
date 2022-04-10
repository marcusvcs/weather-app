import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { CurrentWeatherModel, DailyForecastModel, LocationModel } from './weather.model'


@Injectable({
  providedIn: 'root'
})

// Service to get the weather data from the API
export class WeatherService {

  constructor(private http: HttpClient) { }

  public getLocation(latitude: number, longitude: number) {

    return this.http.get<LocationModel>(`/location?latitude=${latitude}&longitude=${longitude}`)
  }
  
  public getCurrentWeather(locationKey:number) {

    return this.http.get<CurrentWeatherModel>(`/currentweather?locationKey=${locationKey}`)
  }
  public getDailyForecast(locationKey: number) {

    return this.http.get<DailyForecastModel>(`/dailyforecast?locationKey=${locationKey}`)
  }
}
