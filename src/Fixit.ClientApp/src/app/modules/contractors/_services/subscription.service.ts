import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/_services/auth.service';
import {SubscriptionOrderData} from '../_models/subscription/SubscriptionOrderData';
import {CreateSubscriptionData} from '../_models/subscription/CreateSubscriptionData';
import {PaymentResult} from "../_models/subscription/PaymentResult";

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  subscriptionUrl = environment.paymentUrl + 'subscription';
  paymentUrl = environment.paymentUrl + 'payment/process';

  createSubscription(data: CreateSubscriptionData) {
    return this.http.post<SubscriptionOrderData>(this.subscriptionUrl, data);
  }

  processPayment(data: any) {
    return this.http.post<PaymentResult>(this.paymentUrl, data);
  }

  cancelSubscription(subscriptionUUID: string) {
    return this.http.post<PaymentResult>(this.subscriptionUrl + '/' + subscriptionUUID + '/cancel', null);
  }

  reactivateSubscription(subscriptionUUID: string) {
    return this.http.post<PaymentResult>(this.subscriptionUrl + '/' + subscriptionUUID + '/reactivate', null);
  }
}
