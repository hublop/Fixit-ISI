import { SharedModule } from './../shared/shared.module';
import { CustomersRoutingModule } from './customers-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterCustomerComponent } from './register-customer/register-customer.component';
import { AuthModule } from '../auth/auth.module';
import { MaterialModule } from '../material/material.module';

@NgModule({
  imports: [
    CommonModule,
    CustomersRoutingModule,
    AuthModule,
    SharedModule,
    MaterialModule
  ],
  declarations: [
    RegisterCustomerComponent
  ]
})
export class CustomersModule { }
