import { HttpParams, HttpResponse } from '@angular/common/http';

export interface Pagination {
    currentPage: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
}

export function appendPaginationParams(params: HttpParams, page, itemsPerPage): HttpParams {
    if (page && itemsPerPage) {
        params = params.append('pageNumber', page);
        params = params.append('pageSize', itemsPerPage);
    }

    return params;
}

export function getPaginationFromResponse(response: HttpResponse<any>): Pagination {
    let pagination: Pagination = {
        currentPage: 1,
        pageSize: 10,
        totalItems: 0,
        totalPages: 0
    };

    const paginationHeader = response.headers.get('X-Pagination');

    if (paginationHeader) {
        pagination = JSON.parse(paginationHeader);
    }

    return pagination;
}
