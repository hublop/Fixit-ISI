import { PaginatedResult } from './../../shared/pagination/PaginatedResult';
import { CategoryInfoForList } from './../../categories/_models/CategoryInfoForList';
import { ContractorForList } from './../_models/ContractorForList';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '../../shared/pagination/Pagination';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Router } from '@angular/router';
import { ContractorsService } from '../_services/contractors.service';
import { InfoService } from '../../shared/info/info.service';
import { ContractorsListFilter } from '../_models/ContractorsListFilter';

@Component({
  selector: 'app-contractors-list',
  templateUrl: './contractors-list.component.html',
  styleUrls: ['./contractors-list.component.scss']
})
export class ContractorsListComponent implements OnInit {

  categories: CategoryInfoForList[];
  pagination: Pagination;
  contractorsListFilter: ContractorsListFilter;
  contractors: ContractorForList[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contractorsService: ContractorsService,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.getDataFromResolvers();
  }

  getDataFromResolvers(): void {
    this.route.data.subscribe(data => {
      this.categories = data['categories'];
      this.pagination = data['contractors'].pagination;
      this.contractors = data['contractors'].result;
    }, error => {
      this.infoService.error('Nie udało się wczytać danych, spróbuj ponownie.');
    });
  }

  loadContractors() {
    this.contractorsService.getContractors(this.contractorsListFilter, this.pagination.currentPage,
      this.pagination.pageSize).subscribe((paginatedContractors: PaginatedResult<ContractorForList>) => {
        this.contractors = paginatedContractors.result;
        this.pagination = paginatedContractors.pagination;
      }, error => {
        this.infoService.error('Nie udało się wczytać danych, spróbuj ponownie.');
      });
  }

  onFilterChanged(filter) {
    console.log(filter);
    this.contractorsListFilter = filter;
    this.pagination.currentPage = 1;
    this.loadContractors();
  }

  onPageChanged($event) {
    this.pagination.currentPage = $event.pageIndex + 1;
    this.loadContractors();
    window.scroll(0, 0);
  }

}
