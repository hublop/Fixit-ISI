import { CustomerService } from './../_services/customer.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Resolve, ActivatedRouteSnapshot,  RouterStateSnapshot} from '@angular/router';
import { catchError } from 'rxjs/operators';
import { Location } from '../../../../../node_modules/@angular/common';
import {CustomerOrderData} from '../_models/CustomerOrderData';

@Injectable({
  providedIn: 'root'
})
export class CustomerOrdersResolver implements Resolve<CustomerOrderData[]> {
  constructor(
    private customerService: CustomerService,
    private location: Location
  ) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CustomerOrderData[]> {
    return this.customerService.getOrders(route.params.id)
      .pipe(catchError((err => {
        if (err.status === 404) {
          this.location.back();
        }
        return new Observable<CustomerOrderData[]>(null);
      })));
  }

}
