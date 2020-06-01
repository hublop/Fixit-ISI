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
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { EditPersonalDataComponent } from './edit-profile/edit-personal-data/edit-personal-data.component';
import { EditProvidedServicesComponent } from './edit-profile/edit-provided-services/edit-provided-services.component';
import { EditSubscriptionComponent, PaymentContentDialog } from './edit-profile/edit-subscription/edit-subscription.component';
import { AddSubscriptionInfoComponent } from './edit-profile/edit-subscription/add-subscription-info/add-subscription-info.component';
import { AgmCoreModule } from '@agm/core';
import {OrdersComponent} from './orders/orders.component';
import {OrderListElementComponent} from './orders/orders-list-element/order-list-element.component';

@NgModule({
  imports: [
    CommonModule,
    ContractorsRoutingModule,
    MaterialModule,
    SharedModule,
    AuthModule,
    CategoriesModule,
    AgmCoreModule
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
    EditProfileComponent,
    EditPersonalDataComponent,
    EditProvidedServicesComponent,
    EditSubscriptionComponent,
    AddSubscriptionInfoComponent,
    PaymentContentDialog,
    ProvidedServicesComponent,
    ProvidedCategoriesComponent,
    OrdersComponent,
    OrderListElementComponent
  ],
  exports: [
    RegisterContractorComponent
  ]
})
export class ContractorsModule { }
