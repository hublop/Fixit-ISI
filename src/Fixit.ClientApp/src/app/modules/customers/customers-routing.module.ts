import { RegisterCustomerComponent } from './register-customer/register-customer.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

export const customersRoutes: Routes = [
    {
        path: '',
        redirectTo: 'register',
        pathMatch: 'full'
    },
    {
        path: 'register',
        component: RegisterCustomerComponent
    },
    {
        path: '**',
        redirectTo: 'register',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(customersRoutes)],
    exports: [RouterModule]
})
export class CustomersRoutingModule { }
