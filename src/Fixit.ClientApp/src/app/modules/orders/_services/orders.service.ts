import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateOrderData } from '../_models/CreateOrderData';
import {AuthService} from '../../auth/_services/auth.service';
import {PaginatedResult} from '../../shared/pagination/PaginatedResult';
import {ContractorOrder} from '../../contractors/_models/ContractorOrder';
import {Observable} from 'rxjs';
import {OrderAcceptData} from "../_models/OrderAcceptData";

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  baseUrl = environment.apiUrl + 'orders/';

  createDistributedOrder(data: CreateOrderData) {
    return this.http.post(this.baseUrl, data);
  }

  createDirectOrder(contractorId: number, data: CreateOrderData) {
    return this.http.post(this.baseUrl + contractorId, data);
  }

  getContractorOrders(id: number): Observable<ContractorOrder[]> {
    let contractorId = -1;
    const loggedInUser = this.authService.getloggedInUser();
    if (loggedInUser) {
      contractorId = loggedInUser.id;
    }
    return this.http.get<ContractorOrder[]>(this.baseUrl  + contractorId);
  }

  acceptOrder(orderId: number, orderAcceptance: OrderAcceptData) {
    orderAcceptance.contractorId = this.authService.getloggedInUser().id;
    return this.http.put(this.baseUrl + orderId + '/accept', orderAcceptance);
  }
}
