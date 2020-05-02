import { environment } from './../../../../environments/environment';
import { map, catchError, mergeMap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse,
  HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { TokenService } from '../_services/token.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor {

  isRefreshingToken = false;

  constructor(private authService: AuthService, private router: Router, private tokenService: TokenService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authReq = this.authenticateRequest(req, this.tokenService.getAccessToken(), 'Bearer');

    return next.handle(authReq)
      .pipe(map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          return event;
        }
      }))
      .pipe(catchError((error: any) => {
        if (!(error instanceof HttpErrorResponse)) {
          return this.throwResponseError(error);
        }

        if (error.url === environment.apiUrl + 'auth/refresh_token' || error.url === environment.apiUrl + 'auth/token') {
          this.requestLoginRefresh();
          return this.throwResponseError(error);
        }

        if (this.shouldRequestRefreshToken(error)) {
          this.isRefreshingToken = true;
          if (!this.tokenService.getRefreshToken()) {
            this.requestLoginRefresh();
            return this.throwResponseError(error);
          }

          return this.authService.refreshAccessToken()
            .pipe(mergeMap(token => {
              const authReqRepeat = this.authenticateRequest(req, this.tokenService.getAccessToken(), 'Bearer');
              return next.handle(authReqRepeat)
                .pipe(map((event: HttpEvent<any>) => {
                  this.isRefreshingToken = false;
                  return event;
                }))
                .pipe(catchError((err: any) => {
                  this.requestLoginRefresh();
                  return this.throwResponseError(err);
                }));
            }));
        } else {
          return this.throwResponseError(error);
        }
      }));
  }

  authenticateRequest(req: HttpRequest<any>, token: string, tokenType: string): HttpRequest<any> {
    if (token && tokenType) {
      return req.clone({ setHeaders: { Authorization: tokenType + ' ' + token }});
    }
    return req;
  }

  throwResponseError(error): Observable<any> {
    this.isRefreshingToken = false;
    return throwError(error);
  }

  requestLoginRefresh(): void {
    this.tokenService.removeJsonWebToken();
    this.router.navigate(['/login']);
  }

  shouldRequestRefreshToken(error: HttpErrorResponse): boolean {
    return error.status === 401 && !this.isRefreshingToken && !this.tokenService.isAccessTokenValid();
  }
}

export const TokenInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: TokenInterceptor,
  multi: true
};
