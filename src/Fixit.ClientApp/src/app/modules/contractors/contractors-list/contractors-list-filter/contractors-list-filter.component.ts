import { CategoryInfoForList } from './../../../categories/_models/CategoryInfoForList';
import { ContractorsListFilter } from './../../_models/ContractorsListFilter';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { SubcategoryInfoForList } from '../../../categories/_models/SubcategoryInfoForList';

export const _filter = (opt: SubcategoryInfoForList[], value: string): SubcategoryInfoForList[] => {
  const filterValue = value.toLowerCase();
  return opt.filter(x => x.name.toLowerCase().indexOf(value) !== -1);
};

@Component({
  selector: 'app-contractors-list-filter',
  templateUrl: './contractors-list-filter.component.html',
  styleUrls: ['./contractors-list-filter.component.scss']
})
export class ContractorsListFilterComponent implements OnInit {

  listFilterGroup: FormGroup;
  contractorsListFilter: ContractorsListFilter;
  @Output() contractorsListFilterChanged: EventEmitter<ContractorsListFilter>;
  @Input() categories: CategoryInfoForList[];
  options: CategoryInfoForList[];

  constructor(private formBuilder: FormBuilder) {
    this.contractorsListFilterChanged = new EventEmitter<ContractorsListFilter>();
  }

  ngOnInit() {
    this.createForm();

    this.listFilterGroup.get('subcategoryId').valueChanges.subscribe(val => {
      this.options = this._filterGroup(val);
    });
  }

  createForm(): void {
    this.listFilterGroup = this.formBuilder.group({
      subcategoryId: new FormControl(null),
      nameSearchString: new FormControl('')
    });
  }

  applyFilter(): void {
    this.contractorsListFilter = this.listFilterGroup.value;

    let subCatId: number;

    this.categories.forEach(x => {
      x.subCategories.forEach(y => {
        if (y.name === this.listFilterGroup.get('subcategoryId').value) {
          subCatId = y.id;
        }
      });
    });
    if (subCatId) {
      this.contractorsListFilter.subcategoryId = subCatId;
    } else {
      this.contractorsListFilter.subcategoryId = null;
    }
    this.contractorsListFilterChanged.emit(this.contractorsListFilter);
  }

  private _filterGroup(value: string): CategoryInfoForList[] {
    if (value) {
      return this.categories
        .map(group =>
          ({id: group.id, name: group.name, description: group.description,
            subCategories: _filter(group.subCategories, value)} as CategoryInfoForList))
        .filter(group => group.subCategories.length > 0);
    }

    return this.categories;
  }
}
