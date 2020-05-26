import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthCustomerLoginGuard } from '../auth/_guards/customer.guard';
import { CreateOrderComponent } from './create-order/create-order.component';

export const ordersRoutes: Routes = [
    {
        path: '',
        redirectTo: 'new',
        pathMatch: 'full'
    },
    {
        path: 'new',
        component: CreateOrderComponent,
        canActivate: [AuthCustomerLoginGuard]
    },
    {
        path: 'new/:id',
        component: CreateOrderComponent,
        canActivate: [AuthCustomerLoginGuard]
    },
    {
        path: '**',
        redirectTo: 'new',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(ordersRoutes)],
    exports: [RouterModule]
})
export class OrdersRoutingModule { }
