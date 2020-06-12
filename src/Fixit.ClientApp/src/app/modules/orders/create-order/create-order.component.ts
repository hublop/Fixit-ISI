/// <reference types="@types/googlemaps" />
import { Component, OnInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AuthService } from '../../auth/_services/auth.service';
import { OrdersService } from '../_services/orders.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InfoService } from '../../shared/info/info.service';
import { CategoriesService } from '../../categories/_services/categories.service';
import { CategoryInfoForList } from '../../categories/_models/CategoryInfoForList';
import { CreateOrderData } from '../_models/CreateOrderData';
import { ContractorsService } from '../../contractors/_services/contractors.service';
import { ContractorProfile } from '../../contractors/_models/ContractorProfile';
import { MapsAPILoader } from '@agm/core';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss']
})
export class CreateOrderComponent implements OnInit {

  orderForm: FormGroup;
  locationForm: FormGroup;
  categories: CategoryInfoForList[];

  base64Photos: string[] = new Array<string>();

  contractorId: number;
  contractorProfile: ContractorProfile;

  autocomplete: google.maps.places.Autocomplete;

  @ViewChild('search')
  public searchElement: ElementRef;
  private selectedPlaceId = '';
  private selectedLat: number = 0;
  private selectedLng: number = 0;
  isCatLoaded: boolean;
  isContractorLoaded: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private categoriesService: CategoriesService,
    private ordersService: OrdersService,
    private contractorsService: ContractorsService,
    private route: ActivatedRoute,
    private router: Router,
    private infoService: InfoService,
    private mapsApiLoader: MapsAPILoader
  ) { }

  ngOnInit() {
    this.route.params.subscribe(param => {
      this.contractorId = param.id;
    });
    this.buildForm();
    this.isCatLoaded = false;
    this.isContractorLoaded = false;
    this.categoriesService.getAll().subscribe(result => {
      this.isCatLoaded = true;
      this.categories = result;

      this.mapsApiLoader.load().then(() => {
        this.autocomplete = new google.maps.places.Autocomplete(this.searchElement.nativeElement);
        google.maps.event.addListener(this.autocomplete, 'place_changed', () => {
          var place = this.autocomplete.getPlace();
          var place_id = place.place_id;
          var name = place.name;
          var latLng = place.geometry.location;
          this.selectedPlaceId = place_id;
          this.selectedLat = latLng.lat();
          this.selectedLng = latLng.lng();
        });
      });
    }, error => {
      this.infoService.error('Nie udało sie wczytać danych');
    });

    if (this.isDirectCreateMode()) {
      this.contractorsService.getContractorProfile(this.contractorId).subscribe(profile => {
        this.contractorProfile = profile;
        this.isContractorLoaded = true;
      }, error => {
        this.infoService.error('Nie udało sie wczytać danych');
      });
    } else {
      this.isContractorLoaded = true;
    }
  }

  buildForm(): void {
    this.orderForm = this.formBuilder.group({
      description: new FormControl('', [
        Validators.required,
        Validators.minLength(50),
        Validators.maxLength(2000)
      ]),
      subcategoryId: new FormControl(null, [
        Validators.required
      ])
    });

    this.locationForm = this.formBuilder.group({
      placeId: new FormControl('', [
        Validators.required
      ])
    });
  }

  isDirectCreateMode() {
    return this.contractorId;
  }

  getCustomerId(): number {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
      return null;
    }

    return loggedInUser.id;
  }

  getDisplaySpec(): string {
    if (!this.contractorProfile.categories || this.contractorProfile.categories.length === 0) {
      return '';
    }

    let displaySpec = '';

    for (let index = 0; index < this.contractorProfile.categories.length && index < 2; index++) {
      const element = this.contractorProfile.categories[index].name;
      if (index === 0) {
        displaySpec += element;
      } else {
        displaySpec += ', ' + element;
      }
    }

    return displaySpec;
  }

  addPhoto() {
    document.getElementById('filePicker').click();
  }

  removePhoto(photoBase64: string) {
    this.base64Photos = this.base64Photos.filter(photo => photo !== photoBase64);
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
    this.base64Photos.push('data:image/png;base64,' + btoa(binaryString));
  }

  getOrderData(): CreateOrderData {
    const orderData: CreateOrderData = {
      base64Photos: this.base64Photos,
      customerId: this.getCustomerId(),
      description: this.orderForm.controls.description.value,
      placeId: this.selectedPlaceId,
      latitude: this.selectedLat,
      longitude: this.selectedLng,
      subcategoryId: this.orderForm.controls.subcategoryId.value
    };

    return orderData;
  }

  registerOrder() {
    if (!this.orderForm.valid) {
      return;
    }

    const orderData = this.getOrderData();

    if (this.isDirectCreateMode()) {
      this.ordersService.createDirectOrder(this.contractorId, orderData).subscribe(() => {
        this.infoService.error('Złożono zamówienie');
        this.router.navigate(['/']);
      }, error => {
        this.infoService.error('Nie udało się stworzyć zamówienia.');
      });
    } else {
      this.ordersService.createDistributedOrder(orderData).subscribe(() => {
        this.infoService.error('Złożono zamówienie');
        this.router.navigate(['/']);
      }, error => {
        this.infoService.error('Nie udało się stworzyć zamówienia.');
      });
    }
  }
}
