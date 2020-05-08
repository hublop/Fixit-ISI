import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { RegisterContractorData } from '../_models/RegisterContractorData';

@Injectable({
  providedIn: 'root'
})
export class ContractorsService {

  constructor(
    private http: HttpClient
  ) { }

  baseUrl = environment.apiUrl + 'contractors/';

  register(registerData: RegisterContractorData) {
    return this.http.post(this.baseUrl, registerData);
  }
}
