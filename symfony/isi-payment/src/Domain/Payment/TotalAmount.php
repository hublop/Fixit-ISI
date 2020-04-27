<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

class TotalAmount
{
    private int $value;

    public function __construct(int $value)
    {
        if ($value < 0) {
            throw new \InvalidArgumentException('Total amount cannot be less than 0');
        }

        $this->value = $value;
    }

    public function __toString(): string
    {
        return $this->value;
    }
}