import { AuthModule } from '../auth/auth.module';
import { SharedModule } from '../shared/shared.module';
import { OrdersRoutingModule } from './orders-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { ContractorsModule } from '../contractors/contractors.module';
import { CreateOrderComponent } from './create-order/create-order.component';

@NgModule({
  imports: [
    CommonModule,
    OrdersRoutingModule,
    SharedModule,
    MaterialModule,
    AuthModule,
    ContractorsModule
  ],
  declarations: [
    CreateOrderComponent
  ]
})
export class OrdersModule { }
