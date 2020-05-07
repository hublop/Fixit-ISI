<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

class Signature
{
    private string $value;

    public function __construct($value)
    {
        $this->value = $value;
    }

    public static function getForPaymentWidget(PaymentWidget $widget, string $privateKey)
    {
        $values = [
            $widget->getCurrencyCode(),
            $widget->getCustomerEmail(),
            $widget->getCustomerLanguage(),
            $widget->getMerchantPosId(),
            json_encode($widget->getRecurringPayment()),
            $widget->getShopName(),
            json_encode($widget->getStoreCard()),
            $widget->getTotalAmount(),
            $privateKey
        ];
        return new self(hash('sha256', implode('', $values)));
    }

    public function __toString()
    {
        return $this->value;
    }
}