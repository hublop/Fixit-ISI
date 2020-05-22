import { UpdatePhotoData } from './../../_models/UpdatePhotoData';
import { ContractorProfile } from './../../_models/ContractorProfile';
import { ContractorsService } from './../../_services/contractors.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { UpdatePersonalInfoData } from '../../_models/UpdatePersonalInfoData';
import { InfoService } from '../../../shared/info/info.service';

@Component({
  selector: 'app-edit-personal-data',
  templateUrl: './edit-personal-data.component.html',
  styleUrls: ['./edit-personal-data.component.scss']
})
export class EditPersonalDataComponent implements OnInit, OnChanges {

  @Input() contractorProfile: ContractorProfile;
  contractorProfileBeforeEdit: ContractorProfile;

  base64stringPhoto = '';
  display = '';

  @Output() dataHasBeenChanged: EventEmitter<boolean>;

  personalDataFormGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private contractorsService: ContractorsService,
    private infoService: InfoService
  ) {
    this.dataHasBeenChanged = new EventEmitter<boolean>();
  }

  ngOnInit() {
    this.contractorProfileBeforeEdit = this.contractorProfile;
    this.display = this.contractorProfile.imageUrl;
    this.buildForm();
  }

  ngOnChanges() {
    this.contractorProfileBeforeEdit = this.contractorProfile;
    this.display = this.contractorProfile.imageUrl;
    this.buildForm();
  }

  hasChanges(): boolean {
    return this.contractorProfileBeforeEdit.firstName !== this.personalDataFormGroup.controls.firstName.value ||
      this.contractorProfileBeforeEdit.lastName !== this.personalDataFormGroup.controls.lastName.value ||
      this.contractorProfileBeforeEdit.companyName !== this.personalDataFormGroup.controls.companyName.value ||
      this.contractorProfileBeforeEdit.selfDescription !== this.personalDataFormGroup.controls.selfDescription.value ||
      this.contractorProfileBeforeEdit.phoneNumber !== this.personalDataFormGroup.controls.phoneNumber.value ||
      this.contractorProfileBeforeEdit.imageUrl !== this.display;
  }

  buildForm(): void {
    this.personalDataFormGroup = this.formBuilder.group({
      firstName: new FormControl(this.contractorProfile.firstName, [
        Validators.required,
        Validators.maxLength(255)
      ]),
      lastName: new FormControl(this.contractorProfile.lastName, [
        Validators.required,
        Validators.maxLength(255)
      ]),
      companyName: new FormControl(this.contractorProfile.companyName, [
        Validators.maxLength(100)
      ]),
      selfDescription: new FormControl(this.contractorProfile.selfDescription, [
        Validators.maxLength(400)
      ]),
      phoneNumber: new FormControl(this.contractorProfile.phoneNumber, [
        Validators.required,
        Validators.maxLength(100)
      ])
    });
  }

  update() {
    if (!this.personalDataFormGroup.valid) {
      return;
    }

    const personalData: UpdatePersonalInfoData = {
      firstName: this.personalDataFormGroup.controls.firstName.value,
      lastName: this.personalDataFormGroup.controls.lastName.value,
      companyName: this.personalDataFormGroup.controls.companyName.value,
      phoneNumber: this.personalDataFormGroup.controls.phoneNumber.value,
      selfDescription: this.personalDataFormGroup.controls.selfDescription.value,
      locationId: this.personalDataFormGroup.controls.locationId.value
    };

    this.contractorsService.updatePersonalData(personalData).subscribe(result => {
      if (this.contractorProfileBeforeEdit.imageUrl !== this.display) {
        const photoData: UpdatePhotoData = {
          content: this.display,
          fileName: this.contractorProfileBeforeEdit.id + Date.now().toString()
        };

        this.contractorsService.updatePhoto(photoData).subscribe(() => {
          this.notifyChange();
        }, error => {
          this.infoService.error('Nie udało się zmienić zdjęcia.');
        });
      } else {
        this.notifyChange();
      }
    }, error => {
      this.infoService.error('Nie udało się zmienić danych.');
    });
  }

  notifyChange() {
    this.dataHasBeenChanged.emit(true);
  }

  handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];

    if (files && file) {
      const reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    const binaryString = readerEvt.target.result;
    this.base64stringPhoto = btoa(binaryString);
    this.display = 'data:image/png;base64,' + this.base64stringPhoto;
  }

  selectPhoto() {
    document.getElementById('filePicker').click();
  }
}
