import {Component, OnInit, Input, AfterViewInit, Inject, ElementRef, HostListener} from '@angular/core';
import { ContractorProfile } from '../../_models/ContractorProfile';
import { InfoService } from '../../../shared/info/info.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import {Router} from '@angular/router';
import {SubscriptionService} from '../../_services/subscription.service';
import {PayData} from '../../_models/subscription/PayData';
import {CreateSubscriptionData} from '../../_models/subscription/CreateSubscriptionData';
import {SubscriptionOrderData} from '../../_models/subscription/SubscriptionOrderData';
import {DomSanitizer} from '@angular/platform-browser';
import {DOCUMENT} from '@angular/common';
import {PaymentResult} from '../../_models/subscription/PaymentResult';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import {MatDialog} from '@angular/material/dialog';
import {PayResultEvent} from '../../_events/PayResultEvent';
registerLocaleData(localePl, 'pl');
@Component({
  selector: 'app-edit-subscription',
  templateUrl: './edit-subscription.component.html',
  styleUrls: ['./edit-subscription.component.scss']
})
export class EditSubscriptionComponent implements OnInit, AfterViewInit {

  @Input() contractorId: number;
  @Input() contractorProfile: ContractorProfile;

  subscriptionForm: FormGroup;
  payForm: FormGroup;
  payData: PayData;
  createSubscriptionFormData: CreateSubscriptionData;
  orderId = '';
  errors = '';
  subsriptionStatus = '';
  isPremium = false;
  isCancelled = false;
  dialog: any;
  constructor(
    @Inject(DOCUMENT) private document,
    private infoService: InfoService,
    private formBuilder: FormBuilder,
    private subscriptionService: SubscriptionService,
    private router: Router,
    private sanitizer: DomSanitizer,
    private elementRef: ElementRef,
    private matDialog: MatDialog
  ) {
  }
  ngAfterViewInit() {
    const s = this.document.createElement('script');
    const code = 'function paymentResult($data) {' +
      'var event = new CustomEvent("payResult", {detail:$data});' +
      'document.getElementById("subscriptionComponent").dispatchEvent(event);' +
      '}';
    s.type = 'text/javascript';
    s.appendChild(this.document.createTextNode(code));
    const __this = this;
    s.onload = () => { __this.afterScriptAdded(); };
    this.elementRef.nativeElement.appendChild(s);
  }
  afterScriptAdded() {
    const params = {
      width: '350px',
      height: '420px',
    };
    if (typeof (window['functionFromExternalScript']) === 'function') {
      window['functionFromExternalScript'](params);
    }
  }

  @HostListener('payResult', ['$event'])
  public onPayResult(event: PayResultEvent) {
    const data = event.detail;
    data.orderId = this.orderId;
    this.subscriptionService.processPayment(data).subscribe((result: PaymentResult) => {
      this.dialog.close();
      window.location.reload();
    }, error => (result: PaymentResult) => {
      console.log(result);
    });
  }
  ngOnInit() {
    if (this.contractorProfile.subscriptionStatus === 'Active') {
      this.subsriptionStatus = 'Aktywne';
      this.isPremium = true;
      this.isCancelled = false;
    } else if (this.contractorProfile.subscriptionStatus === 'Cancelled') {
      this.subsriptionStatus = 'Anulowane';
      this.isPremium = true;
      this.isCancelled = true;
    } else {
      this.subsriptionStatus = 'Nieaktywne';
      this.isPremium = false;
    }

    if (!this.isPremium) {
      this.createForm();
    }
  }
  createForm() {
    this.subscriptionForm = this.formBuilder.group({
      firstname: new FormControl(this.contractorProfile.firstName, [
        Validators.required,
        Validators.maxLength(255)
      ]),
      lastname: new FormControl(this.contractorProfile.lastName, [
        Validators.required,
        Validators.maxLength(255)
      ]),
      userEmail: new FormControl(this.contractorProfile.email, [
        Validators.required,
        Validators.email,
        Validators.maxLength(255)
      ])
    });
    this.payForm = this.formBuilder.group({
      orderId: new FormControl(),
      paymentToken: new FormControl(),
      maskedCard: new FormControl(),
      type: new FormControl()
    });
  }
  subscribe() {

    if (!this.subscriptionForm.valid) {
      return;
    }
    this.dialog = this.openDialog();
    this.createSubscriptionFormData = this.subscriptionForm.value;
    this.createSubscriptionFormData.cost = 2000;
    this.createSubscriptionFormData.userId = this.contractorProfile.contractorUUID;
    window.setTimeout(() =>
      this.subscriptionService.createSubscription(this.createSubscriptionFormData).subscribe((data: SubscriptionOrderData) => {
      this.orderId = data.orderId;
      this.loadPaymentWidget(data);
      window.setTimeout(() => document.getElementById('pay-button').click(), 3000);
    }, error => {
      this.errors = error;
    }), 400);
  }

  loadPaymentWidget(data: SubscriptionOrderData) {
    const head = document.getElementsByTagName('head')[0];
    const script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://secure.snd.payu.com/front/widget/js/payu-bootstrap.js';
    script.async = false;
    for (const [key, value] of Object.entries(data['widget-data'])) {
      script.setAttribute(key, value);
    }
    head.appendChild(script);
  }
  openDialog() {
    return this.matDialog.open(PaymentContentDialog);
  }
  cancelSubscription() {
    this.subscriptionService.cancelSubscription(this.contractorProfile.subscriptionUUID).subscribe((result: PaymentResult) => {
      window.location.reload();
    }, error => (result: PaymentResult) => {
      window.location.reload();
    });
    return '';
  }

  reactivateSubscription() {
    this.subscriptionService.reactivateSubscription(this.contractorProfile.subscriptionUUID).subscribe((result: PaymentResult) => {
      window.location.reload();
    }, error => (result: PaymentResult) => {
      console.log(result);
    });
    return '';
  }
}
@Component({
  selector: 'app-payment-content-dialog',
  templateUrl: 'payment-content-dialog.html',
})
export class PaymentContentDialog {}

