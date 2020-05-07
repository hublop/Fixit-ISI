<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment\Gateway;

use App\Domain\Payment\PaymentResult;

interface GatewayInterface
{
    public function create(array $order, bool $isFirst): PaymentResult;
}