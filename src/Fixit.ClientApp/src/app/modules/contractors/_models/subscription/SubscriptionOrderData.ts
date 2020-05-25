import {PaymentWidgetData} from './PaymentWidgetData';

export interface SubscriptionOrderData {
  action: string;
  orderId: string;
  subscriptionId: string;
  totalAmount: string;
  userId: string;
  'widget-data': PaymentWidgetData;
}
