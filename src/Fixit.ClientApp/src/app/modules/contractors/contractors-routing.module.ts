import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { CategoriesListResolver } from '../categories/_resolvers/categories-list.resolver';
import { ContractorsListResolver } from './_resolvers/contractors-list.resolver';

export const contractorsRoutes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    },
    {
        path: 'register',
        component: RegisterContractorComponent
    },
    {
        path: 'list',
        component: ContractorsListComponent,
        resolve: {
            categories: CategoriesListResolver,
            contractors: ContractorsListResolver
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(contractorsRoutes)],
    exports: [RouterModule]
})
export class ContractorsRoutingModule { }
