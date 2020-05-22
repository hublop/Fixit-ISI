import { CustomerService } from './../_services/customer.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { CustomerPersonalData } from '../_models/CustomerPersonalData';
import { Location } from '../../../../../node_modules/@angular/common';

@Injectable({
    providedIn: 'root'
})
export class CustomerPersonalDataResolver implements Resolve<CustomerPersonalData> {
    constructor(
        private customerService: CustomerService,
        private location: Location
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<CustomerPersonalData> {
        return this.customerService.getPersonalData(route.params.id)
        .pipe(catchError((err => {
            if (err.status === 404) {
                this.location.back();
            }
            return new Observable<CustomerPersonalData>(null);
        })));
    }
}
