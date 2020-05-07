<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Infrastructure\PaymentGateway\PayU;

use App\Domain\Payment\Gateway\GatewayInterface;
use App\Domain\Payment\PaymentResult;
use App\Domain\Payment\PaymentToken;
use App\Domain\Payment\Status;
use OpenPayU_Result;

class PayuOrder implements GatewayInterface
{
    public function __construct(PayUConfigurator $configurator)
    {
        $configurator->configurePayment();
    }

    public function create(array $order, bool $isFirst = true): PaymentResult
    {
        try {
            /** @var OpenPayU_Result $response */
            $response = \OpenPayU_Order::create($order);
        } catch (\OpenPayU_Exception $exception) {
            return new PaymentResult(Status::error(), null, '', $exception->getMessage());
        }
        if (\OpenPayU_Order::SUCCESS !== $response->getStatus()) {
            return new PaymentResult(Status::error(), new PaymentToken(''), null);
        }
        if ($isFirst) {
            $token = new PaymentToken($response->getResponse()->payMethods->payMethod->value);
            $card = $response->getResponse()->payMethods->payMethod->card->number;
        } else {
            $token =  new PaymentToken($order['payMethods']['payMethod']['value']);
            $card = '';
        }
        return new PaymentResult(
            Status::success(),
            $token,
            $card
        );

    }
}