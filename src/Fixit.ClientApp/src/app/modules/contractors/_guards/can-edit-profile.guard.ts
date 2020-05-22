import { AuthService } from './../../auth/_services/auth.service';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Location } from '../../../../../node_modules/@angular/common';
@Injectable({
  providedIn: 'root'
})
export class EditProfileGuard implements CanActivate {

  constructor(
      private authService: AuthService,
      private location: Location
    ) {}

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.authService.isLoggedIn()) {
      return false;
    }

    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
        return false;
    }

    if (loggedInUser.id.toString() !== route.params.id) {
      this.location.back();
      return false;
    }

    return true;
  }
}
