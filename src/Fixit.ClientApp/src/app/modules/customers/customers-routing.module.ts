import { RegisterCustomerComponent } from './register-customer/register-customer.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CustomerPersonalDataResolver } from './_resolvers/customer-profile.resolver';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileGuard } from '../contractors/_guards/can-edit-profile.guard';
import {MyOrdersComponent} from "./my-orders/my-orders.component";
import {AuthCustomerGuard} from "../auth/_guards/auth.guard";
import {CustomerOrdersResolver} from "./_resolvers/customer-orders.resolver";

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
        path: 'profile/:id/edit',
        component: ProfileComponent,
        canActivate: [EditProfileGuard],
        resolve: {
            profile: CustomerPersonalDataResolver
        }
    },
    {
      path: 'profile/:id/orders',
      component: MyOrdersComponent,
      canActivate: [AuthCustomerGuard],
      resolve: {
        orders: CustomerOrdersResolver
      }
    },
    {
      path: ':id/orders',
      component: MyOrdersComponent,
      canActivate: [EditProfileGuard],
      resolve: {
        profile: CustomerPersonalDataResolver
      }
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
