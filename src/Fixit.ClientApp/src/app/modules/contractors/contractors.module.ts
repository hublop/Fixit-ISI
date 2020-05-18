import { AuthModule } from './../auth/auth.module';
import { ContractorsRoutingModule } from './contractors-routing.module';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { SharedModule } from '../shared/shared.module';
import { CategoriesModule } from '../categories/categories.module';
import { RegisterInfoComponent } from './register-contractor/register-info/register-info.component';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { ContractorsListElementComponent } from './contractors-list/contractors-list-element/contractors-list-element.component';
import { ContractorsListFilterComponent } from './contractors-list/contractors-list-filter/contractors-list-filter.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileHeaderComponent } from './profile/profile-header/profile-header.component';
import { OpinionsComponent } from './profile/opinions/opinions.component';
import { OpinionsHeaderComponent } from './profile/opinions/opinions-header/opinions-header.component';
import { OpinionsListComponent } from './profile/opinions/opinions-list/opinions-list.component';
import { ProvidedServicesComponent } from './profile/provided-services/provided-services.component';
import { ProvidedCategoriesComponent } from './profile/provided-services/provided-categories/provided-categories.component';
import { ListElementComponent } from './profile/opinions/opinions-list/list-element/list-element.component';

@NgModule({
  imports: [
    CommonModule,
    ContractorsRoutingModule,
    MaterialModule,
    SharedModule,
    AuthModule,
    CategoriesModule
  ],
  declarations: [
    RegisterContractorComponent,
    RegisterInfoComponent,
    ContractorsListComponent,
    ContractorsListElementComponent,
    ContractorsListFilterComponent,
    ProfileComponent,
    ProfileHeaderComponent,
    OpinionsComponent,
    OpinionsHeaderComponent,
    OpinionsListComponent,
    ListElementComponent,
    ProvidedServicesComponent,
    ProvidedCategoriesComponent
  ],
  exports: [
    RegisterContractorComponent
  ]
})
export class ContractorsModule { }
