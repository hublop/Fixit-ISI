/// <reference types="@types/googlemaps" />
import { CategoryInfoForList } from './../../../categories/_models/CategoryInfoForList';
import { ContractorsListFilter } from './../../_models/ContractorsListFilter';
import { Component, OnInit, Output, EventEmitter, Input, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { SubcategoryInfoForList } from '../../../categories/_models/SubcategoryInfoForList';
import { MapsAPILoader } from '@agm/core';

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

  autocomplete: google.maps.places.Autocomplete;

  @ViewChild('search')
  public searchElement: ElementRef;
  private selectedPlaceId: string = '';

  constructor(private formBuilder: FormBuilder,
    private mapsApiLoader: MapsAPILoader) {
    this.contractorsListFilterChanged = new EventEmitter<ContractorsListFilter>();
  }

  ngOnInit() {
    this.createForm();

    this.listFilterGroup.get('subcategoryId').valueChanges.subscribe(val => {
      this.options = this._filterGroup(val);
    });

    this.mapsApiLoader.load().then(() => {
      this.autocomplete = new google.maps.places.Autocomplete(this.searchElement.nativeElement);
      console.log('autocomplete type ' + (this.autocomplete instanceof google.maps.places.Autocomplete));
      google.maps.event.addListener(this.autocomplete, 'place_changed', () => {
        var place = this.autocomplete.getPlace();
        this.selectedPlaceId = place.place_id;
      });
    });
  }

  createForm(): void {
    this.listFilterGroup = this.formBuilder.group({
      subcategoryId: new FormControl(null),
      nameSearchString: new FormControl(''),
      placeId: new FormControl(this.selectedPlaceId)
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
    if (this.listFilterGroup.get('placeId').value !== null) {
      this.contractorsListFilter.placeId = this.selectedPlaceId;
    }
    this.contractorsListFilterChanged.emit(this.contractorsListFilter);
  }

  private _filterGroup(value: string): CategoryInfoForList[] {
    if (value) {
      return this.categories
        .map(group =>
          ({
            id: group.id, name: group.name, description: group.description,
            subCategories: _filter(group.subCategories, value)
          } as CategoryInfoForList))
        .filter(group => group.subCategories.length > 0);
    }

    return this.categories;
  }
}
