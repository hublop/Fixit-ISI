import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateOrderData } from '../_models/CreateOrderData';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.apiUrl + 'orders/';

  createDistributedOrder(data: CreateOrderData) {
    return this.http.post(this.baseUrl, data);
  }

  createDirectOrder(contractorId: number, data: CreateOrderData) {
    return this.http.post(this.baseUrl + contractorId, data);
  }
}
