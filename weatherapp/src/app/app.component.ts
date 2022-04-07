import { Component, OnInit } from '@angular/core';
import { GeolocationService } from '@ng-web-apis/geolocation';
import { take } from 'rxjs/operators';
import { LocationModel } from './location.model'
import { WeatherService } from './weather.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  public locationModel: LocationModel = {} as LocationModel;
  
  constructor(private readonly geolocation$: GeolocationService, private weatherService: WeatherService) {

  }
  ngOnInit() {
    this.geolocation$.pipe(take(1)).subscribe(position => {

      this.weatherService.getLocation(position.coords.latitude, position.coords.longitude).subscribe(loc => {
        this.locationModel = loc;
      });
    });

  }
  title = 'weatherapp';

}

