import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {OrderAcceptData} from '../../../../orders/_models/OrderAcceptData';

@Component({
  selector: 'app-order-take-offer-popup',
  templateUrl: './order-take-offer-popup.component.html',
})
export class OrderTakeOfferPopupComponent {

  constructor(
    public dialogRef: MatDialogRef<OrderAcceptData>,
    @Inject(MAT_DIALOG_DATA) public data: OrderAcceptData
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
  updateAmount(event) {
    this.data.predictedPrice = event.target.value;
  }


}
