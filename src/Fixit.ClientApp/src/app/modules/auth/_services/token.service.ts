import { JsonWebTokenUser } from '../_models/JsonWebTokenUser';
import { JsonWebToken } from '../_models/JsonWebToken';
import { Injectable } from '@angular/core';
import { RefreshTokenData } from '../_models/RefreshTokenData';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  getAccessToken(): string {
    const jwt = this.getJsonWebToken();

    if (jwt) {
      return jwt.accessToken;
    }

    return null;
  }

  getRefreshToken(): RefreshTokenData {
    const jwt = this.getJsonWebToken();

    if (jwt) {
      const token: RefreshTokenData = {
        refreshToken: jwt.refreshToken.token,
        userId: this.getLoggedInUser().id
      };

      return token;
    }

    return null;
  }

  isAccessTokenValid(): boolean {
    const jwt = this.getJsonWebToken();

    if (!jwt) {
      return false;
    }

    const tokenExpireDate = new Date(jwt.expiresIn);
    const actualDate = new Date(Date.now());

    return tokenExpireDate > actualDate;
  }

  saveJsonWebToken(token: JsonWebToken): void {
    if (!token) {
      return;
    }
    localStorage.setItem('token', JSON.stringify(token));
  }

  removeJsonWebToken(): void {
    localStorage.removeItem('token');
  }

  getLoggedInUser(): JsonWebTokenUser {
    const jwt = this.getJsonWebToken();

    if (jwt) {
      return jwt.user;
    }

    return null;
  }

  getJsonWebToken(): JsonWebToken {
    const jwt = localStorage.getItem('token');

    if (jwt) {
      return JSON.parse(jwt);
    }
  }

}
