import { ChangePasswordComponent } from './change-password/change-password.component';
import { LoginComponent } from './login/login.component';
import { TokenInterceptorProvider } from './_interceptors/token-interceptor';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    LoginComponent,
    ChangePasswordComponent
  ],
  exports: [
    LoginComponent,
    ChangePasswordComponent
  ],
  providers: [
    TokenInterceptorProvider
  ]
})
export class AuthModule { }
