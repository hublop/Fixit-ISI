<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

class MerchantPosId
{
    private int $value;
    public function __construct(int $value)
    {
        if (!is_int($value)) {
            throw new \InvalidArgumentException('Merchant ID must be an integer.');
        }
        $this->value = $value;
    }

    public function __toString()
    {
        return (string) $this->value;
    }
}