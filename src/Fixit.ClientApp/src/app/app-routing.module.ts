import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginGuard } from './modules/auth/_guards/login.guard';
import { LoginComponent } from './modules/auth/login/login.component';


export const routes: Routes = [
  {
      path: '',
      redirectTo: 'login',
      pathMatch: 'full'
  },
  {
      path: 'login',
      canActivate: [LoginGuard],
      component: LoginComponent
  },
  {
      path: '**',
      redirectTo: 'contractors/list',
      pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
