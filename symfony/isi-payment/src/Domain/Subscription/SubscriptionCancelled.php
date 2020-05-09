<?php
/**
 * @category    Fixit-ISI
 * @date        09/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Subscription;

use App\Common\DomainEvent;

class SubscriptionCancelled implements DomainEvent, \JsonSerializable
{
    private SubscriptionId $subscriptionId;
    private UserId $customerId;
    private Status $status;
    private \DateTimeImmutable $createdAt;

    /**
     * SubscriptionActivated constructor.
     * @param SubscriptionId     $subscriptionId
     * @param UserId             $customerId
     * @param Status             $status
     * @param \DateTimeImmutable $createdAt
     */
    public function __construct(
        SubscriptionId $subscriptionId,
        UserId $customerId,
        Status $status,
        \DateTimeImmutable $createdAt
    ) {
        $this->subscriptionId  = $subscriptionId;
        $this->customerId      = $customerId;
        $this->status          = $status;
        $this->createdAt       = $createdAt;
    }

    public function jsonSerialize()
    {
        return [
            'subscriptionId' => (string) $this->subscriptionId,
            'customerId' => (string) $this->customerId,
            'status' => (string) $this->status,
            'createdAt' => $this->createdAt
        ];
    }
}