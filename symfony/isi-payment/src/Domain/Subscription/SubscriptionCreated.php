<?php
/**
 * @category    Fixit-ISI
 * @date        10/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Subscription;

use App\Common\DomainEvent;
use App\Domain\Order\Order;

class SubscriptionCreated implements DomainEvent
{
    public Subscription $subscription;
    public Order $order;

    public function __construct(Subscription $subscription, Order $order)
    {
        $this->subscription = $subscription;
        $this->order        = $order;
    }
}