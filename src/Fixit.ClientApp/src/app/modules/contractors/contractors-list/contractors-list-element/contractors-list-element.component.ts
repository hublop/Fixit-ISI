import { Router } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { ContractorForList } from '../../_models/ContractorForList';
import { AuthService } from '../../../auth/_services/auth.service';

@Component({
  selector: 'app-contractors-list-element',
  templateUrl: './contractors-list-element.component.html',
  styleUrls: ['./contractors-list-element.component.scss']
})
export class ContractorsListElementComponent implements OnInit {

  @Input() contractor: ContractorForList;
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  getDisplaySpec(): string {
    if (!this.contractor.specializations || this.contractor.specializations.length === 0) {
      return '';
    }

    let displaySpec = '';

    for (let index = 0; index < this.contractor.specializations.length && index < 2; index++) {
      const element = this.contractor.specializations[index];
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

  navigateToProfile(contractor: ContractorForList) {
    this.router.navigate(['/contractors/profile/' + contractor.id]);
  }

  addOrderButtonVisible(): boolean {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser || this.authService.isCustomer()) {
      return true;
    }

    return false;
  }

  navigateToAddOrder() {
    if (this.authService.isLoggedIn() && this.authService.getloggedInUser() &&
      this.authService.getloggedInUser().id === this.contractor.id) {
      return;
    }
    this.router.navigate(['/orders/new/' + this.contractor.id]);
  }
}
