import { RegisterCustomerData } from './../_models/RegisterCustomerData';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.apiUrl + 'customers/';

  register(registerData: RegisterCustomerData) {
    return this.http.post(this.baseUrl, registerData);
  }
}
