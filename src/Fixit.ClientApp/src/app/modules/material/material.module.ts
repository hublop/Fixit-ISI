import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDividerModule } from '@angular/material/divider';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTooltipModule } from '@angular/material/tooltip';
import {MatDialogModule} from '@angular/material/dialog';

@NgModule({
  imports: [
    CommonModule,
    MatTooltipModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatListModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatDividerModule,
    MatTabsModule,
    MatExpansionModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatStepperModule,
    MatDialogModule,
  ],
  exports: [
    MatTooltipModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatListModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatDividerModule,
    MatTabsModule,
    MatExpansionModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatStepperModule,
    MatDialogModule
  ],
  declarations: []
})
export class MaterialModule { }
