/// <reference types="@types/googlemaps" />
import { AuthService } from './../../../auth/_services/auth.service';
import { Router } from '@angular/router';
import { ContractorProfile } from './../../_models/ContractorProfile';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { MapsAPILoader } from '@agm/core';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss']
})
export class ProfileHeaderComponent implements OnInit {

  @Input() contractor: ContractorProfile;

  locationNameEmitter: EventEmitter<string> = new EventEmitter();

  constructor(
    private router: Router,
    private authService: AuthService,
    private mapsApiLoader: MapsAPILoader
  ) { }

  ngOnInit() {
    this.locationNameEmitter.subscribe(result => {
      document.getElementById("placeName").textContent = result + ', ';
    });
    this.mapsApiLoader.load().then(() => {
      var geocoder = new google.maps.Geocoder;
      var placeId = this.contractor.placeId;
      if (placeId !== null) {
        this.geocodePlaceId(this.locationNameEmitter, geocoder, placeId);
      }
    });
  }

  getDisplaySpec(): string {
    if (!this.contractor.categories || this.contractor.categories.length === 0) {
      return '';
    }

    let displaySpec = '';

    for (let index = 0; index < this.contractor.categories.length && index < 2; index++) {
      const element = this.contractor.categories[index].name;
      if (index === 0) {
        displaySpec += element;
      } else {
        displaySpec += ', ' + element;
      }
    }

    return displaySpec;
  }

  getStarStyle(star) {
    const styles = {
      'color': star <= this.contractor.avgRating ? '#0074D9' : 'lightgray',
      'fill': star <= this.contractor.avgRating ? '#0074D9' : 'lightgray'
    };

    return styles;
  }

  getOpinionsCount() {
    return this.contractor.opinions ? this.contractor.opinions.length : 0;
  }

  addOrderButtonVisible(): boolean {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser || this.authService.isCustomer()) {
      return true;
    }

    return false;
  }

  navigateToAddOrder(): void {
    if (this.authService.isLoggedIn() && this.authService.getloggedInUser() &&
      this.authService.getloggedInUser().id === this.contractor.id) {
      return;
    }
    this.router.navigate(['/orders/new/' + this.contractor.id]);
  }

  geocodePlaceId(emitter, geocoder, placeId) {
    geocoder.geocode({ 'placeId': placeId }, function (results, status) {
      if (status === 'OK') {
        if (results[0]) {
          emitter.emit(results[0].formatted_address);
        } else {
          window.alert('No results found');
        }
      } else {
        //window.alert('Geocoder failed due to: ' + status);
      }
    });
  }
}
