import { MaterialModule } from './../material/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopNavComponent } from './top-nav/top-nav.component';
import { AuthModule } from '../auth/auth.module';
import { InfoComponent } from './info/info.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [
    TopNavComponent,
    InfoComponent
  ],
  exports: [
    TopNavComponent,
    FormsModule,
    ReactiveFormsModule,
    InfoComponent
  ]
})
export class SharedModule { }
