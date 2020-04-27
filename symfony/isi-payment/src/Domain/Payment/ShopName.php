<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

class ShopName
{
    private string $value;

    public function __construct(string $value)
    {
        if (!$value) {
            throw new \InvalidArgumentException('Shop name must not be empty');
        }
        $this->value = $value;
    }

    public function __toString()
    {
        return $this->value;
    }
}