import { Observable } from 'rxjs';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryInfoForList } from '../_models/CategoryInfoForList';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl + 'categories/';

  getAll(): Observable<CategoryInfoForList[]> {
    return this.http.get<CategoryInfoForList[]>(this.baseUrl);
  }
}
