import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';

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
        component: ContractorsListComponent
    },
    {
        path: '**',
        redirectTo: 'list',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(contractorsRoutes)],
    exports: [RouterModule]
})
export class ContractorsRoutingModule { }
