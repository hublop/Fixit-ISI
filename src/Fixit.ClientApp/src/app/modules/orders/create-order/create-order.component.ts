import { Component, OnInit } from '@angular/core';
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

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private categoriesService: CategoriesService,
    private ordersService: OrdersService,
    private contractorsService: ContractorsService,
    private route: ActivatedRoute,
    private router: Router,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(param =>  {
      this.contractorId = param.id;
    });
    this.buildForm();
    this.categoriesService.getAll().subscribe(result => {
      this.categories = result;
    }, error => {
      this.infoService.error('Nie udało sie wczytać danych');
    });

    if (this.isDirectCreateMode()) {
      this.contractorsService.getContractorProfile(this.contractorId).subscribe(profile => {
        this.contractorProfile = profile;
      }, error => {
        this.infoService.error('Nie udało sie wczytać danych');
      });
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
      placeId: this.locationForm.controls.placeId.value,
      subcategoryId: this.orderForm.controls.subcategoryId.value
    };

    return orderData;
  }

  registerOrder() {
    if (!this.orderForm.valid) {
      return;
    }

    const orderData = this.getOrderData();

    if (this.isDirectCreateMode()){
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
