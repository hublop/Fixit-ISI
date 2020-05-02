import { ChangePasswordData } from './../_models/ChangePasswordData';
import { environment } from './../../../../environments/environment';
import { Router } from '@angular/router';
import { JsonWebToken } from '../_models/JsonWebToken';
import { UserLoginData } from '../_models/UserLoginData';
import { Injectable } from '@angular/core';
import { TokenService } from './token.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { RefreshTokenData } from '../_models/RefreshTokenData';
import { JsonWebTokenUser } from '../_models/JsonWebTokenUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private router: Router
  ) { }

  baseUrl = environment.apiUrl + 'auth/';

  login(loginData: UserLoginData) {
      return this.http.post<JsonWebToken>(this.baseUrl + 'token', loginData,
        {headers: new HttpHeaders().set('Content-Type', 'application/json') })
        .pipe(map((token: JsonWebToken) => {
          this.tokenService.saveJsonWebToken(token);
          return token;
      }));
  }

  refreshAccessToken() {
    const refreshToken: RefreshTokenData = this.tokenService.getRefreshToken();
    return this.http.post<JsonWebToken>(this.baseUrl + 'refresh_token', refreshToken,
    {headers: new HttpHeaders().set('Content-Type', 'application/json') })
    .pipe(map((token: JsonWebToken) => {
      this.tokenService.saveJsonWebToken(token);
      return token;
    }));
  }

  changePassword(data: ChangePasswordData) {
    const loggedInUser = this.getloggedInUser();
    if (!loggedInUser) {
      return;
    }

    return this.http.put(this.baseUrl + loggedInUser.id, data);
  }

  logout() {
    this.tokenService.removeJsonWebToken();
    this.router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return this.tokenService.isAccessTokenValid() || this.tokenService.getRefreshToken() !== null;
  }

  isContractor() {
    return this.tokenService.getLoggedInUser().roles.findIndex(x => x === 'Contractor') !== -1;
  }

  isCustomer() {
    return this.tokenService.getLoggedInUser().roles.findIndex(x => x === 'Customer') !== -1;
  }

  getloggedInUser(): JsonWebTokenUser {
    return this.tokenService.getLoggedInUser();
  }

}
