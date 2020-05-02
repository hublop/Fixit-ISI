import { AuthService } from './../_services/auth.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChangePasswordData } from '../_models/ChangePasswordData';
import { InfoService } from '../../shared/info/info.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  editPasswordForm: FormGroup;
  newPasswordVisible = false;
  newPasswordRepeatVisible = false;
  oldPasswordVisible = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.editPasswordForm = this.formBuilder.group({
      oldPassword: new FormControl('', [
        Validators.required,
        Validators.pattern('(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$')
      ]),
      newPasswordRepeat: new FormControl('', [
        Validators.required,
        Validators.pattern('(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$')
      ]),
      newPassword: new FormControl('', [
        Validators.required,
        Validators.pattern('(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$')
      ])
    });
  }

  areNewPasswordsEqual(): boolean {
    return this.editPasswordForm.controls.newPassword.value === this.editPasswordForm.controls.newPasswordRepeat.value;
  }

  update() {
    if (!this.editPasswordForm.valid) {
      return;
    }

    const changePasswordData: ChangePasswordData = {
      oldPassword: this.editPasswordForm.controls.oldPassword.value,
      newPassword: this.editPasswordForm.controls.newPassword.value
    };

    this.authService.changePassword(changePasswordData).subscribe(result => {
      this.authService.logout();
      this.router.navigate(['/login']);
    }, error => {
      this.infoService.error('Nie udało się zmienić hasła, spróbuj ponownie.');
    });
  }

}
