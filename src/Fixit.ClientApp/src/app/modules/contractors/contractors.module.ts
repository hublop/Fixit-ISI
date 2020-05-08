import { AuthModule } from './../auth/auth.module';
import { ContractorsRoutingModule } from './contractors-routing.module';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { SharedModule } from '../shared/shared.module';
import { CategoriesModule } from '../categories/categories.module';

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
    RegisterContractorComponent
  ],
  exports: [
    RegisterContractorComponent
  ]
})
export class ContractorsModule { }
