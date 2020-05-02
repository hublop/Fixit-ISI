import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class InfoService {

  constructor(
    public snackBar: MatSnackBar
  ) { }

  success(message: string) {
    this.snackBar.open(message, 'Ok', {
      duration: 7000
    });
  }

  info(message: string) {
    this.snackBar.open(message, 'Ok', {
      duration: 7000
    });
  }

  error(message: string) {
    this.snackBar.open(message, 'Ok', {
      duration: 7000
    });
  }
}
