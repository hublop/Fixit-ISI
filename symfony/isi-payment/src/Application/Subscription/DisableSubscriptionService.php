<?php
/**
 * @category    Fixit-ISI
 * @date        10/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Application\Subscription;

use App\Common\Clock;
use App\Common\Result;
use App\Domain\Subscription\Status;
use App\Domain\Subscription\Subscription;
use App\Domain\Subscription\SubscriptionDeactivated;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Component\Messenger\MessageBusInterface;

class DisableSubscriptionService
{
    private EntityManagerInterface $entityManager;
    private MessageBusInterface $messageBus;

    public function __construct(EntityManagerInterface $entityManager, MessageBusInterface $messageBus)
    {
        $this->entityManager = $entityManager;
        $this->messageBus = $messageBus;
    }

    public function disableSubscription(Subscription $subscription)
    {
        $subscription->disable();
        $this->entityManager->persist($subscription);
        $this->entityManager->flush();
        $event = new SubscriptionDeactivated($subscription->id(),
            $subscription->getUserId(),
            Status::disabled(),
            Clock::system()->currentDateTime()
        );
        $this->messageBus->dispatch($event);
        return Result::success([
            $event
        ]);
    }
}