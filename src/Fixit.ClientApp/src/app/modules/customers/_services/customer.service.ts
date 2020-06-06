import { AuthService } from './../../auth/_services/auth.service';
import { RegisterCustomerData } from './../_models/RegisterCustomerData';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from '../../../../../node_modules/rxjs';
import { CustomerPersonalData } from '../_models/CustomerPersonalData';
import { UpdateCustomerPersonalData } from '../_models/UpdateCustomerPersonalData';
import {CustomerOrderData} from '../_models/CustomerOrderData';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  baseUrl = environment.apiUrl + 'customers/';

  register(registerData: RegisterCustomerData) {
    return this.http.post(this.baseUrl, registerData);
  }

  getPersonalData(id): Observable<CustomerPersonalData> {
    return this.http.get<CustomerPersonalData>(this.baseUrl + id);
  }

  updatePersonalData(data: UpdateCustomerPersonalData) {
    const loggedInUser = this.authService.getloggedInUser();

    if (!loggedInUser) {
      return;
    }

    return this.http.put(this.baseUrl + loggedInUser.id, data);
  }

  getOrders(id: any): Observable<CustomerOrderData[]> {
    return this.http.get<CustomerOrderData[]>(environment.apiUrl + 'Orders?customerId=' + id);
  }
}
