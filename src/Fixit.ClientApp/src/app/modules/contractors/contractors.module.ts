import { AuthModule } from './../auth/auth.module';
import { ContractorsRoutingModule } from './contractors-routing.module';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { SharedModule } from '../shared/shared.module';
import { CategoriesModule } from '../categories/categories.module';
import { RegisterInfoComponent } from './register-contractor/register-info/register-info.component';

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
    RegisterInfoComponent
  ],
  exports: [
    RegisterContractorComponent
  ]
})
export class ContractorsModule { }
