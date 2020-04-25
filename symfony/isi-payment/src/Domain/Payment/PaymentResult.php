<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

class PaymentResult
{
    private ?PaymentToken $token;
    private string $cardNumber;
    private Status $status;
    private string $message;

    public function __construct(Status $status, ?PaymentToken $token, string $cardNumber, string $message = '')
    {
        $this->token      = $token;
        $this->cardNumber = $cardNumber;
        $this->status     = $status;
        $this->message    = $message;
    }

    /**
     * @return Status
     */
    public function getStatus(): Status
    {
        return $this->status;
    }

    /**
     * @return PaymentToken|null
     */
    public function getToken(): ?PaymentToken
    {
        return $this->token;
    }

    /**
     * @return string
     */
    public function getCardNumber(): string
    {
        return $this->cardNumber;
    }

    /**
     * @return string
     */
    public function getMessage(): string
    {
        return $this->message;
    }
}