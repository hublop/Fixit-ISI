import { Injectable } from '@angular/core';
import { CategoryInfoForList } from '../_models/CategoryInfoForList';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { CategoriesService } from '../_services/categories.service';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class CategoriesListResolver implements Resolve<CategoryInfoForList[]> {

    constructor(
        private categoriesService: CategoriesService
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<CategoryInfoForList[]> {
        return this.categoriesService.getAll().pipe(catchError((err => {
            return new Observable<CategoryInfoForList[]>(null);
        })));
    }
}
