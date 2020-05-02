import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { Location } from '../../../../../node_modules/@angular/common';

@Injectable({
    providedIn: 'root'
  })
  export class AuthCustomerLoginGuard implements CanActivate {

    constructor(
      private authService: AuthService,
      private router: Router,
      private location: Location
    ) {}

    canActivate(): Observable<boolean> | Promise<boolean> | boolean {
      if (this.authService.isLoggedIn() && this.authService.isCustomer()) {
        return true;
      }

      this.router.navigate(['login']);
      return false;
    }
  }