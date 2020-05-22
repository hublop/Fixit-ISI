import { CategoryInEditServices } from './../../_models/CategoryInEditServices';
import { CategoriesService } from './../../../categories/_services/categories.service';
import { Component, OnInit, Input } from '@angular/core';
import { ContractorProfile } from '../../_models/ContractorProfile';
import { ContractorsService } from '../../_services/contractors.service';
import { CategoryInfoForList } from '../../../categories/_models/CategoryInfoForList';
import { SubcategoryInEditServices } from '../../_models/SubcategoryInEditServices';
import { InfoService } from '../../../shared/info/info.service';

@Component({
  selector: 'app-edit-provided-services',
  templateUrl: './edit-provided-services.component.html',
  styleUrls: ['./edit-provided-services.component.scss']
})
export class EditProvidedServicesComponent implements OnInit {

  @Input() contractorId: number;

  openedPanel = 0;

  @Input() contractorProfile: ContractorProfile;
  categoriesEditInfo: CategoryInEditServices[];

  @Input() allCategories: CategoryInfoForList[];

  constructor(
    private categoriesService: CategoriesService,
    private contractorsService: ContractorsService,
    private infoService: InfoService
  ) {
    this.categoriesEditInfo = new Array<CategoryInEditServices>();
  }

  ngOnInit() {
    this.assignCategoriesEditInfo();
  }

  openPanel(index: number) {
    this.openedPanel = index;
  }

  toggleSubcategory(subcategoryId: number, categoryId: number) {
    const subcategory = this.categoriesEditInfo.find(x => x.id === categoryId).subCategories.find(y => y.id === subcategoryId);
    if (subcategory.isProvided) {
      this.unprovideSubcategory(subcategory);
    } else {
      this.provideSubcategory(subcategory);
    }
  }

  provideSubcategory(subcategory: SubcategoryInEditServices) {
    this.contractorsService.provideRepairService(subcategory.id).subscribe(result => {
      subcategory.isProvided = true;
    }, error => {
      this.infoService.error('Nie udało się zapisać zmian.');
    });

  }

  unprovideSubcategory(subcategory: SubcategoryInEditServices) {
    this.contractorsService.unprovideRepairService(subcategory.id).subscribe(result => {
      subcategory.isProvided = false;
    }, error => {
      this.infoService.error('Nie udało się zapisać zmian.');
    });
  }

  getProvidedSubcategoriesCount(category: CategoryInEditServices): number {
    return this.categoriesEditInfo.find(x => x.id === category.id).subCategories.filter(x => x.isProvided === true).length;
  }

  assignCategoriesEditInfo() {
    if (!this.contractorProfile || !this.contractorProfile.categories || this.contractorProfile.categories.length === 0) {
      this.categoriesEditInfo = new Array<CategoryInEditServices>();
    }

    this.categoriesEditInfo = this.allCategories.map(category => ({
      id: category.id,
      description: category.description,
      name: category.name,
      subCategories: category.subCategories.map(subcategory => ({
        id: subcategory.id,
        name: subcategory.name,
        description: subcategory.description,
        categoryId: category.id,
        isProvided: this.isProvided(category.id, subcategory.id)
      }) as SubcategoryInEditServices)
    }) as CategoryInEditServices);
  }

  isProvided(categoryId, subcategoryId) {
    const category = this.contractorProfile.categories.find(x => x.id === categoryId);
    if (!category) {
      return false;
    }

    return category.subcategories.findIndex(x => x.id === subcategoryId) !== -1;
  }

}
