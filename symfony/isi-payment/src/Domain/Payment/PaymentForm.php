<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use App\Common\Email;
use App\Common\Url;
use App\Common\UUID;

/**
 * Class PaymentForm
 * @package App\Domain\Payment
 */
class PaymentForm implements \JsonSerializable
{
    private Url $action;
    private UUID $orderId;
    private UUID $subscriptionId;
    private PaymentWidget $widget;

    public function __construct(
        Url $action,
        UUID $orderId,
        UUID $subscriptionId,
        PaymentWidget $paymentWidget
    ) {
        $this->action = $action;
        $this->subscriptionId = $subscriptionId;
        $this->orderId = $orderId;
        $this->widget = $paymentWidget;
    }


    public function jsonSerialize(): array
    {
        return [
            'orderId' => (string) $this->orderId,
            'subscriptionId' => (string) $this->subscriptionId,
            'action' => (string) $this->action,
            'widget-data' => $this->widget->jsonSerialize()
        ];
    }
}