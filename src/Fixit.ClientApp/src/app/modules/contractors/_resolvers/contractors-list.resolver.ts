import { PaginatedResult } from './../../shared/pagination/PaginatedResult';
import { ContractorForList } from './../_models/ContractorForList';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ContractorsService } from '../_services/contractors.service';

@Injectable({
    providedIn: 'root'
})
export class ContractorsListResolver implements Resolve<PaginatedResult<ContractorForList>> {

    defaultPageSize = 5;
    defaultPageNumber = 1;

    constructor(
        private contractorsService: ContractorsService
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<ContractorForList>> {
        return this.contractorsService.getContractors(null, this.defaultPageNumber, this.defaultPageSize)
        .pipe(catchError((err => {
            return new Observable<PaginatedResult<ContractorForList>>(null);
        })));
    }
}
