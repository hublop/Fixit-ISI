import { Router } from '@angular/router';
import { AuthService } from './../../auth/_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.scss']
})
export class TopNavComponent implements OnInit {

  isMenuVisible: boolean;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  goToMainPage() {
    this.router.navigate(['']);
  }

  isNavVisible() {
    return this.router.url !== '/login';
  }

  goToLogin(): void {
    if (this.isLoggedIn()) {
      return;
    }

    this.hideMenu();
    this.router.navigate(['/login']);
  }

  logout(): void {
    this.authService.logout();
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  goToProfile(): void {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
      return;
    }
    this.hideMenu();
    const navigateUrl = '/contractors/profile/' + loggedInUser.id;
    this.router.navigate([navigateUrl]);
  }

  goToContractorsOrders() {
    if (!this.isContractor()) {
      return;
    }

    const navigateUrl = '/contractors/' + this.authService.getloggedInUser().id + '/orders';
    this.router.navigate([navigateUrl]);
    this.hideMenu();
  }

  goToContractorsRegisterForm() {
    if (this.isLoggedIn()) {
      return;
    }
    this.hideMenu();
    this.router.navigate(['contractors/register']);
  }

  goToCreateOrder() {
    this.hideMenu();
    this.router.navigate(['orders/new']);
  }

  goToCustomerRegisterForm() {
    if (this.isLoggedIn()) {
      return;
    }
    this.hideMenu();
    this.router.navigate(['/customers/register']);
  }

  getUserName() {
    const user = this.authService.getloggedInUser();

    if (!user) {
      return '';
    }

    return user.firstName + ' ' + user.lastName;
  }

  goToContractorsList() {
    this.hideMenu();
    this.router.navigate(['contractors/list']);
  }

  goToSettings() {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
      return;
    }
    this.hideMenu();
    const prefixUrl = this.authService.isContractor() ? '/contractors' : 'customers';
    const navigateUrl = prefixUrl + '/profile/' + loggedInUser.id + '/edit';
    this.router.navigate([navigateUrl]);
  }

  showMenu() {
    this.isMenuVisible = true;
  }

  hideMenu() {
    this.isMenuVisible = false;
  }

  toggleMenu() {
    this.isMenuVisible = !this.isMenuVisible;
  }

  isContractor() {
    return this.authService.isLoggedIn() && this.authService.isContractor();
  }
}
