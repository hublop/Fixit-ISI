import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginGuard } from './modules/auth/_guards/login.guard';
import { LoginComponent } from './modules/auth/login/login.component';


export const routes: Routes = [
  {
    path: '',
    redirectTo: 'contractors',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'contractors/list',
    pathMatch: 'full'
  },
  {
    path: 'customers',
    runGuardsAndResolvers: 'always',
    loadChildren: () => import('./modules/customers/customers.module').then(m => m.CustomersModule)
  },
  {
    path: 'contractors',
    runGuardsAndResolvers: 'always',
    loadChildren: () => import('./modules/contractors/contractors.module').then(m => m.ContractorsModule)
  },
  {
    path: 'login',
    canActivate: [LoginGuard],
    component: LoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
