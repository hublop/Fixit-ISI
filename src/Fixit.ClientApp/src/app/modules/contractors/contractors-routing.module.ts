import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';

export const contractorsRoutes: Routes = [
    {
        path: 'register',
        component: RegisterContractorComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(contractorsRoutes)],
    exports: [RouterModule]
})
export class ContractorsRoutingModule { }
