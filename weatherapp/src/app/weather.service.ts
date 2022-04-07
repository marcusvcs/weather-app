import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { LocationModel } from './location.model'


@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpClient) { }

  public getLocation(latitude: number, longitude: number) {

    return this.http.get<LocationModel>(`/location?latitude=${latitude}&longitude=${longitude}`)
  }
}
