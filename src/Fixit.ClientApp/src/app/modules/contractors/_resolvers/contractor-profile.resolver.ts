import { ContractorProfile } from './../_models/ContractorProfile';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ContractorsService } from '../_services/contractors.service';
import { Location } from '../../../../../node_modules/@angular/common';

@Injectable({
    providedIn: 'root'
})
export class ContractorProfileResolver implements Resolve<ContractorProfile> {
    constructor(
        private contractorsService: ContractorsService,
        private location: Location
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ContractorProfile> {
        return this.contractorsService.getContractorProfile(route.params.id)
        .pipe(catchError((err => {
            if (err.status === 404) {
                this.location.back();
            }
            return new Observable<ContractorProfile>(null);
        })));
    }
}
