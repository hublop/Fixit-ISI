import { AuthService } from '../../auth/_services/auth.service';
import { ContractorProfile } from '../_models/ContractorProfile';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { InfoService } from '../../shared/info/info.service';
import {ContractorOrder} from '../_models/ContractorOrder';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders: ContractorOrder[];

  constructor(
    private route: ActivatedRoute,
    private infoService: InfoService
  ) {
  }
  ngOnInit(): void {
      this.getOrdersFromResolver();
    }

  getOrdersFromResolver(): void {
      this.route.data.subscribe(data => {
        this.orders = data['orders'];
      }, error => {
        this.infoService.error('Nie udało się wczytać danych');
      });
    }

  getsortData() {
    return this.orders.sort((a, b) => {
      return  (new Date(b.creationDate) as any) - (new Date(a.creationDate) as any);
    });
  }
}
