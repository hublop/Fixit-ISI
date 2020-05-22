import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CustomerPersonalData } from './../../_models/CustomerPersonalData';
import { Component, OnInit, Input, EventEmitter, Output, OnChanges } from '@angular/core';
import { UpdateCustomerPersonalData } from '../../_models/UpdateCustomerPersonalData';
import { CustomerService } from '../../_services/customer.service';
import { InfoService } from '../../../shared/info/info.service';

@Component({
  selector: 'app-edit-personal-data-customer',
  templateUrl: './edit-personal-data.component.html',
  styleUrls: ['./edit-personal-data.component.scss']
})
export class EditPersonalDataComponent implements OnInit, OnChanges {

  @Input() customerProfile: CustomerPersonalData;

  customerProfileBeforeEdit: CustomerPersonalData;

  @Output() dataHasBeenChanged: EventEmitter<boolean>;

  personalDataFormGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private customerService: CustomerService,
    private infoService: InfoService
  ) {
    this.dataHasBeenChanged = new EventEmitter<boolean>();
  }

  ngOnInit() {
    this.customerProfileBeforeEdit = this.customerProfile;
    this.buildForm();
  }

  ngOnChanges() {
    this.customerProfileBeforeEdit = this.customerProfile;
    this.buildForm();
  }

  hasChanges(): boolean {
    return this.customerProfileBeforeEdit.firstName !== this.personalDataFormGroup.controls.firstName.value ||
      this.customerProfileBeforeEdit.lastName !== this.personalDataFormGroup.controls.lastName.value ||
      this.customerProfileBeforeEdit.phoneNumber !== this.personalDataFormGroup.controls.phoneNumber.value;
  }

  buildForm(): void {
    this.personalDataFormGroup = this.formBuilder.group({
      firstName: new FormControl(this.customerProfile.firstName, [
        Validators.required,
        Validators.maxLength(255)
      ]),
      lastName: new FormControl(this.customerProfile.lastName, [
        Validators.required,
        Validators.maxLength(255)
      ]),
      phoneNumber: new FormControl(this.customerProfile.phoneNumber, [
        Validators.required,
        Validators.maxLength(100)
      ])
    });
  }

  update() {
    if (!this.personalDataFormGroup.valid) {
      return;
    }

    const personalData: UpdateCustomerPersonalData = {
      firstName: this.personalDataFormGroup.controls.firstName.value,
      lastName: this.personalDataFormGroup.controls.lastName.value,
      phoneNumber: this.personalDataFormGroup.controls.phoneNumber.value
    };

    this.customerService.updatePersonalData(personalData).subscribe(result => {
      this.notifyChange();
    }, error => {
      this.infoService.error('Nie udało się zaktualizować danych.');
    });
  }

  notifyChange() {
    this.dataHasBeenChanged.emit(true);
  }
}
