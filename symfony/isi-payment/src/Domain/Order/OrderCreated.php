<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Order;

use App\Common\DomainEvent;
use App\Common\Url;
use App\Domain\Payment\PaymentWidget;
use App\Domain\Subscription\SubscriptionId;

class OrderCreated implements DomainEvent, \JsonSerializable
{
    private OrderId $orderId;
    private SubscriptionId $subscriptionId;
    private Url $action;
    private PaymentWidget $widgetData;

    public function __construct(
        OrderId $orderId,
        SubscriptionId $subscriptionId,
        Url $url,
        PaymentWidget $paymentWidget
    ) {
        $this->orderId = $orderId;
        $this->subscriptionId = $subscriptionId;
        $this->action = $url;
        $this->widgetData = $paymentWidget;
    }

    public function jsonSerialize()
    {
        return [
            'orderId' => (string) $this->orderId,
            'subscriptionId' => (string) $this->subscriptionId,
            'action' => (string) $this->action,
            'widget-data' => $this->widgetData->jsonSerialize()
        ];
    }
}