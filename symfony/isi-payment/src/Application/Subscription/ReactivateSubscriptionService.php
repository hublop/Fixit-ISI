<?php

namespace App\Application\Subscription;

use App\Application\Order\OrderFacade;
use App\Common\Result;
use App\Common\SystemClock;
use App\Common\UUID;
use App\Domain\Subscription\Status;
use App\Domain\Subscription\Subscription;
use App\Domain\Subscription\SubscriptionActivated;
use App\Domain\Subscription\SubscriptionCancelled;
use App\Infrastructure\Doctrine\SubscriptionRepository;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Component\Messenger\MessageBusInterface;
use Symfony\Component\Messenger\Transport\AmqpExt\AmqpStamp;

final class ReactivateSubscriptionService
{
    private SubscriptionRepository $subscriptionRepository;
    private EntityManagerInterface $entityManager;
    private MessageBusInterface $messageBus;
    private OrderFacade $orderFacade;

    public function __construct(
        SubscriptionRepository $subscriptionRepository,
        EntityManagerInterface $entityManager,
        MessageBusInterface $messageBus,
        OrderFacade $orderFacade
    ) {
        $this->subscriptionRepository = $subscriptionRepository;
        $this->entityManager          = $entityManager;
        $this->messageBus             = $messageBus;
        $this->orderFacade            = $orderFacade;
    }

    public function reactivateSubscription(UUID $uuid)
    {
        $subscription = $this->subscriptionRepository->findByUUID($uuid);
        if (!$subscription instanceof Subscription) {
            return Result::failure(sprintf('Subscription with UUID \'%s\' was not found', (string)$uuid), 404);
        }
        $result = $subscription->reactivate();
        if (!$result instanceof Result\Success) {
            return $result;
        }
        $this->entityManager->persist($subscription);
        $this->entityManager->flush();
        $this->messageBus->dispatch(
            new SubscriptionActivated(
                $subscription->id(),
                $subscription->getUserId(),
                Status::active(),
                $subscription->getNextPaymentDate(),
                SystemClock::system()->currentDateTime()
            )
        );
        return Result::success();
    }
}