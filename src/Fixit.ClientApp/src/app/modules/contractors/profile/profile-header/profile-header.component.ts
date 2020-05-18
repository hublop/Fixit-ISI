import { AuthService } from './../../../auth/_services/auth.service';
import { Router } from '@angular/router';
import { ContractorProfile } from './../../_models/ContractorProfile';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss']
})
export class ProfileHeaderComponent implements OnInit {

  @Input() contractor: ContractorProfile;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
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
}
