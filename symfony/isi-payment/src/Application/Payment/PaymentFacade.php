<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Application\Payment;

use App\Common\IP;
use App\Common\Result;
use App\Domain\Order\OrderId;
use App\Domain\Payment\PaymentToken;

class PaymentFacade
{
    private ProcessPaymentService $processPaymentService;

    public function __construct(ProcessPaymentService $processPaymentService)
    {
        $this->processPaymentService = $processPaymentService;
    }

    public function processPayment(OrderId $orderId, IP $customerIP, PaymentToken $paymentToken): Result
    {
        return $this->processPaymentService->processPayment($orderId, $customerIP, $paymentToken);
    }
}