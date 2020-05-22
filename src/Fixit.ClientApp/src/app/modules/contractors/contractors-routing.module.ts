import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegisterContractorComponent } from './register-contractor/register-contractor.component';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { CategoriesListResolver } from '../categories/_resolvers/categories-list.resolver';
import { ContractorsListResolver } from './_resolvers/contractors-list.resolver';
import { ProfileComponent } from './profile/profile.component';
import { ContractorProfileResolver } from './_resolvers/contractor-profile.resolver';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { EditProfileGuard } from './_guards/can-edit-profile.guard';

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
        path: 'profile/:id',
        component: ProfileComponent,
        resolve: {
            profile: ContractorProfileResolver
        }
    },
    {
        path: 'profile/:id/edit',
        canActivate: [EditProfileGuard],
        component: EditProfileComponent,
        resolve: {
            profile: ContractorProfileResolver,
            categories: CategoriesListResolver
        }
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
