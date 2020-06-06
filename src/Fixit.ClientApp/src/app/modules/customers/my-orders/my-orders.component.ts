import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/customer.service';
import { InfoService } from '../../shared/info/info.service';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerOrderData} from '../_models/CustomerOrderData';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent implements OnInit {

  orders: CustomerOrderData[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService,
    private infoService: InfoService
  ) { }
  ngOnInit() {
    this.getOrders();
  }


  private getOrders() {
    this.route.data.subscribe(data => {
      this.orders = data.orders;
      console.log(this.orders);
    }, error => {
      this.infoService.error('Nie udało się wczytać danych.');
    });
  }

  getsortData() {
    return this.orders.sort((a, b) => {
      return  (new Date(b.creationDate) as any) - (new Date(a.creationDate) as any);
    });
  }
}
