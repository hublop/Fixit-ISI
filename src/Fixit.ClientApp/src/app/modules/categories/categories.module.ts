import { AuthModule } from './../auth/auth.module';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesListResolver } from './_resolvers/categories-list.resolver';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AuthModule
  ],
  declarations: [
  ],
  exports: [
  ],
  providers: [
    CategoriesListResolver
  ]
})
export class CategoriesModule { }
