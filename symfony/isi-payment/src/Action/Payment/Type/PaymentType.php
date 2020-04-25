<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Action\Payment\Type;

use App\Common\IP;
use App\Common\Type\AbstractType;
use App\Common\UUID;
use App\Domain\Order\OrderId;
use App\Domain\Payment\PaymentToken;
use Symfony\Component\HttpFoundation\Request;

class PaymentType extends AbstractType
{
    public string $tokenType;
    public PaymentToken $paymentToken;
    public string $maskedCard;
    public string $type;
    public OrderId $orderId;
    public IP $ip;

    public function setTokenType(string $token): self
    {
        $this->tokenType = $token;
        return $this;
    }

    public function setValue(string $value): self
    {
        $this->paymentToken = new PaymentToken($value);
        return $this;
    }

    public function setMaskedCard(string $value): self
    {
        $this->maskedCard = $value;
        return $this;
    }

    public function setType(string $value): self
    {
        $this->type = $value;
        return $this;
    }

    public function setOrderId(string $uuid): self
    {
        $this->orderId = new OrderId(new UUID($uuid));
        return $this;
    }

    public function setIP(Request $request): self
    {
        $this->ip = new IP($request->getClientIp());
        return $this;
    }

    protected function getAdditionalParams(): array
    {
        return ['IP'];
    }

}