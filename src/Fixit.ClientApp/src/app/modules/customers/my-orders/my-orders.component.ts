import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/customer.service';
import { CustomerPersonalData } from '../_models/CustomerPersonalData';
import { FormControl } from '@angular/forms';
import { InfoService } from '../../shared/info/info.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent implements OnInit {
  ngOnInit(): void {
  }

}
