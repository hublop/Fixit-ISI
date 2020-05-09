<?php
/**
 * @category    Fixit-ISI
 * @date        07/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use App\Common\DomainEvent;

class PaymentSucceeded implements DomainEvent, \JsonSerializable
{
    public function jsonSerialize()
    {
        // TODO: Implement jsonSerialize() method.
    }
}