import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {ContractorOrder} from '../../_models/ContractorOrder';
import {AgmGeocoder} from '@agm/core';
import {GeocoderRequest} from '@agm/core/services/google-maps-types';
import {MatDialog} from '@angular/material/dialog';
import {OrderTakeOfferPopupComponent} from './order-take-offer-popup/order-take-offer-popup.component';
import {OrderAcceptData} from '../../../orders/_models/OrderAcceptData';
import {OrdersService} from '../../../orders/_services/orders.service';
@Component({
  selector: 'app-orders-list-element',
  templateUrl: './order-list-element.component.html',
  styleUrls: ['./order-list-element.component.scss']
})
export class OrderListElementComponent implements OnInit {

  @Input() order: ContractorOrder;
  address: string;
  constructor(
    private route: ActivatedRoute,
    private mal: AgmGeocoder,
    private dialog: MatDialog,
    private ordersService: OrdersService
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

  takeOffer() {
    const dialogRef = this.dialog.open(OrderTakeOfferPopupComponent, {
      width: '250px',
      data: {} as OrderAcceptData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.ordersService.acceptOrder(this.order.id, result).subscribe(() => this.order.status = true);
      }
    });

  }
}
