import { ContractorProfile } from './../_models/ContractorProfile';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ContractorsService } from '../_services/contractors.service';
import { Location } from '../../../../../node_modules/@angular/common';
import {ContractorOrder} from '../_models/ContractorOrder';
import {OrdersService} from '../../orders/_services/orders.service';

@Injectable({
  providedIn: 'root'
})
export class ContractorOrdersResolver implements Resolve<ContractorOrder[]> {
  constructor(
    private orderService: OrdersService,
    private location: Location,
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<ContractorOrder[]> {
    return this.orderService.getContractorOrders(route.params.id)
      .pipe(catchError((err => {
        if (err.status === 404) {
          this.location.back();
        }
        return new Observable<ContractorOrder[]>(null);
      })));
  }
}
