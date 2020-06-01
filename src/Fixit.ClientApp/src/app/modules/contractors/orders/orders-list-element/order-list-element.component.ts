import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {ContractorForList} from '../../_models/ContractorForList';
import {ContractorOrder} from '../../_models/ContractorOrder';

@Component({
  selector: 'app-orders-list-element',
  templateUrl: './order-list-element.component.html',
  styleUrls: ['./order-list-element.component.scss']
})
export class OrderListElementComponent implements OnInit {

  @Input() order: ContractorOrder;
  constructor(
    private route: ActivatedRoute,
  ) {
  }
  ngOnInit(): void {
  }

  hasImage(): boolean {
    return this.order.photoUrls.length > 0;
  }

}
