import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserLoginData } from '../_models/UserLoginData';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginData: UserLoginData;
  loginForm: FormGroup;
  error: string;

  constructor(
    private router: Router,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.error = null;
    this.createForm();
  }

  login() {
    if (!this.loginForm.valid) {
      return;
    }

    this.loginData = this.loginForm.value;
    this.authService.login(this.loginData).subscribe(data => {
      this.router.navigate(['/']);
     }, error => {
      this.error = 'Niepoprawna nazwa użytkownika i/lub hasło.';
    });
  }

  createForm() {
    this.loginForm = this.formBuilder.group({
      email: new FormControl('', [
        Validators.required
      ]),
      password: new FormControl('', [
        Validators.required
      ])
    });
  }

  redirectToRegisterCustomer() {
    this.router.navigate(['/customers/register']);
  }

}
