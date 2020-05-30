/// <reference types="@types/googlemaps" />
import { UpdatePhotoData } from './../../_models/UpdatePhotoData';
import { ContractorProfile } from './../../_models/ContractorProfile';
import { ContractorsService } from './../../_services/contractors.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter, OnChanges, ElementRef, ViewChild } from '@angular/core';
import { UpdatePersonalInfoData } from '../../_models/UpdatePersonalInfoData';
import { InfoService } from '../../../shared/info/info.service';
import { MapsAPILoader } from '@agm/core';
import { emit } from 'cluster';

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

  autocomplete: google.maps.places.Autocomplete;

  @ViewChild('search')
  public searchElement: ElementRef;
  locationNameEmitter: EventEmitter<string> = new EventEmitter();

  private selectedPlaceId: string = '';
  private selectedPlaceName: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private contractorsService: ContractorsService,
    private infoService: InfoService,
    private mapsApiLoader: MapsAPILoader
  ) {
    this.dataHasBeenChanged = new EventEmitter<boolean>();
  }

  ngOnInit() {
    this.locationNameEmitter.subscribe(result => {
      this.personalDataFormGroup.get('placeId').setValue(result);
    });
    this.contractorProfileBeforeEdit = this.contractorProfile;
    this.display = this.contractorProfile.imageUrl;
    this.selectedPlaceId = this.contractorProfile.placeId;
    this.buildForm();

    this.mapsApiLoader.load().then(() => {
      this.autocomplete = new google.maps.places.Autocomplete(this.searchElement.nativeElement);
      console.log('autocomplete type ' + (this.autocomplete instanceof google.maps.places.Autocomplete));
      google.maps.event.addListener(this.autocomplete, 'place_changed', () => {
        var place = this.autocomplete.getPlace();
        var place_id = place.place_id;
        var name = place.name;
        var latLng = place.geometry.location;
        console.log("Autocomplete result: " + name + ", id: " + place_id + ", location: " + latLng);
        this.selectedPlaceId = place_id;
        this.selectedPlaceName = name;
        this.locationNameEmitter.emit(name);
      });

      console.log('starting placeid: ' + this.contractorProfile.placeId);
      var geocoder = new google.maps.Geocoder;
      this.geocodePlaceId(this.locationNameEmitter, geocoder, this.contractorProfile.placeId);
    });
  }

  geocodePlaceId(emitter, geocoder, placeId) {
    geocoder.geocode({ 'placeId': placeId }, function (results, status) {
      if (status === 'OK') {
        if (results[0]) {
          emitter.emit(results[0].formatted_address);
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }
    });
  }

  ngOnChanges() {
    this.contractorProfileBeforeEdit = this.contractorProfile;
    this.display = this.contractorProfile.imageUrl;
    this.selectedPlaceId = this.contractorProfile.placeId;
    this.buildForm();
  }

  hasChanges(): boolean {
    return this.contractorProfileBeforeEdit.firstName !== this.personalDataFormGroup.controls.firstName.value ||
      this.contractorProfileBeforeEdit.lastName !== this.personalDataFormGroup.controls.lastName.value ||
      this.contractorProfileBeforeEdit.companyName !== this.personalDataFormGroup.controls.companyName.value ||
      this.contractorProfileBeforeEdit.selfDescription !== this.personalDataFormGroup.controls.selfDescription.value ||
      this.contractorProfileBeforeEdit.phoneNumber !== this.personalDataFormGroup.controls.phoneNumber.value ||
      this.contractorProfileBeforeEdit.imageUrl !== this.display ||
      this.contractorProfileBeforeEdit.placeId !== this.selectedPlaceId;
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
      ]),
      placeId: new FormControl(this.selectedPlaceName, []),
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
      placeId: this.selectedPlaceId
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
