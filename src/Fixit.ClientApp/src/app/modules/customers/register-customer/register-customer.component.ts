import { Router } from '@angular/router';
import { RegisterCustomerData } from './../_models/RegisterCustomerData';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/customer.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.scss']
})
export class RegisterCustomerComponent implements OnInit {

  registerData: RegisterCustomerData;
  registerForm: FormGroup;
  passwordVisible = false;
  errors = '';

  constructor(
    private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit() {
    this.createForm();
  }

  createForm(): void {
    this.registerForm = this.formBuilder.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.maxLength(255)
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.maxLength(255)
      ]),
      email: new FormControl('', [
        Validators.required,
        Validators.email,
        Validators.maxLength(255)
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern('(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$')
      ]),
      phoneNumber: new FormControl('', [
        Validators.required,
        Validators.maxLength(100)
      ])
    });
  }

  register() {
    if (!this.registerForm.valid) {
      return;
    }

    this.registerData = this.registerForm.value;
    this.customerService.register(this.registerData).subscribe(() => {
      this.router.navigate(['/login']);
    }, error => {
      this.errors = 'Użytkownik z podanym adresem e-mail już istnieje';
    });
  }
}
