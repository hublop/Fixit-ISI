<?php
/**
 * @category    symfony
 * @date        02/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Application\Payment;

use App\Domain\Payment\LanguageCode;
use App\Domain\Payment\MerchantPosId;
use App\Domain\Payment\PaymentWidget;
use App\Domain\Payment\ShopName;
use App\Domain\Order;

class WidgetService
{
    private array $configuration;

    public function __construct($payuConfig)
    {
        $this->configuration = $payuConfig;
    }

    public function getPaymentWidget(Order\Order $order): PaymentWidget
    {
        return PaymentWidget::fromOrder(
            $order,
            new MerchantPosId($this->configuration['posId']),
            new ShopName($this->configuration['shopName']),
            LanguageCode::pl(),
            true,
            true,
            $this->configuration['privateKey']
        );
    }
}