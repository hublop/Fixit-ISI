import { CategoryInProfile } from './../../../_models/CategoryInProfile';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-provided-categories',
  templateUrl: './provided-categories.component.html',
  styleUrls: ['./provided-categories.component.scss']
})
export class ProvidedCategoriesComponent implements OnInit {

  @Input() providedCategory: CategoryInProfile;

  constructor() { }

  ngOnInit() {
  }

}
