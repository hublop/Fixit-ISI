import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { RegisterContractorData } from '../_models/RegisterContractorData';
import { ContractorsListFilter } from '../_models/ContractorsListFilter';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../../shared/pagination/PaginatedResult';
import { ContractorForList } from '../_models/ContractorForList';
import { appendPaginationParams, getPaginationFromResponse } from '../../shared/pagination/Pagination';
import { map } from 'rxjs/operators';
import { ContractorProfile } from '../_models/ContractorProfile';

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

  getContractorProfile(id): Observable<ContractorProfile> {
    return this.http.get<ContractorProfile>(this.baseUrl + id);
  }
  
  getContractors(contractorsListFilter?: ContractorsListFilter, page?, itemsPerPage?): Observable<PaginatedResult<ContractorForList>> {
    let params: HttpParams = new HttpParams();
    params = appendPaginationParams(params, page, itemsPerPage);
    params = this.appendContractorsListFilterParams(params, contractorsListFilter);

    return this.http.get<any>(this.baseUrl, {observe: 'response', params: params}).pipe(map(response => {
      const result: PaginatedResult<ContractorForList> = {
        result: response.body,
        pagination: getPaginationFromResponse(response)
      };

      return result;
    }));
  }

  appendContractorsListFilterParams(params: HttpParams, contractorsListFilter?: ContractorsListFilter): HttpParams {
    if (!contractorsListFilter) {
      return params;
    }

    if (contractorsListFilter.placeId) {
      params = params.append('placeId', contractorsListFilter.placeId.toString());
    }

    if (contractorsListFilter.nameSearchString) {
      params = params.append('nameSearchString', contractorsListFilter.nameSearchString);
    }

    if (contractorsListFilter.subcategoryId) {
      params = params.append('subcategoryId', contractorsListFilter.subcategoryId.toString());
    }

    return params;
  }
}
