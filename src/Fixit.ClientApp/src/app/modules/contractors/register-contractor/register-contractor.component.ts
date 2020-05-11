import { Component, OnInit } from '@angular/core';
import { RegisterContractorData } from '../_models/RegisterContractorData';
import { FormGroup, FormBuilder, FormControl, Validators } from '../../../../../node_modules/@angular/forms';
import { ContractorsService } from '../_services/contractors.service';
import { Router } from '@angular/router';
import { InfoService } from '../../shared/info/info.service';

@Component({
  selector: 'app-register-contractor',
  templateUrl: './register-contractor.component.html',
  styleUrls: ['./register-contractor.component.scss']
})
export class RegisterContractorComponent implements OnInit {

  locations: Array<Location>;
  registerData: RegisterContractorData;
  registerForm: FormGroup;
  passwordVisible = false;
  errors = '';

  constructor(
    private formBuilder: FormBuilder,
    private contractorsService: ContractorsService,
    private router: Router,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
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
      companyName: new FormControl('', [
        Validators.maxLength(100)
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
    this.contractorsService.register(this.registerData).subscribe(() => {
      this.router.navigate(['/login']);
    }, error => {
      this.errors = 'Użytkownik z podanym adresem e-mail już istnieje';
    });
  }
}
