import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AgmGeocoder} from '@agm/core';
import {GeocoderRequest} from '@agm/core/services/google-maps-types';
import {MatDialog} from '@angular/material/dialog';
import {OrdersService} from '../../../orders/_services/orders.service';
import {CustomerOrderData} from '../../_models/CustomerOrderData';
import {ContractorForList} from '../../../contractors/_models/ContractorForList';
@Component({
  selector: 'app-customer-orders-list-element',
  templateUrl: './my-order-list-element.component.html',
  styleUrls: ['./my-order-list-element.component.scss']
})
export class MyOrderListElementComponent implements OnInit {

  @Input() order: CustomerOrderData;
  address: string;
  constructor(
    private route: ActivatedRoute,
    private mal: AgmGeocoder,
    private dialog: MatDialog,
    private ordersService: OrdersService,
    private router: Router
  ) {
  }
  ngOnInit(): void {
    const r = {placeId: this.order.placeId} as GeocoderRequest;
    this.mal.geocode(r).subscribe((data) => {
      const result = data.length > 0 ? data[0].formatted_address : 'Nieznany';
      document.getElementById('place-' + this.order.id).textContent = result;
    });
  }

  hasImage(): boolean {
    return this.order.photoUrls.length > 0;
  }

  hasOffers() {
    return this.order.orderOffers.length > 0;
  }

  navigateToProfile(id: number) {
    this.router.navigate(['/contractors/profile/' + id]);
  }
}
