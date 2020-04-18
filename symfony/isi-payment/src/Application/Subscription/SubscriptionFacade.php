<?php

namespace App\Application\Subscription;

use App\Common\Result;
use App\Common\UUID;
use App\Domain\Subscription\Status;
use App\Domain\Subscription\Subscription;
use App\Infrastructure\SubscriptionRepository;

final class SubscriptionFacade
{
    private CreateSubscriptionService $createSubscriptionService;
    /**
     * @var SubscriptionRepository
     */
    private SubscriptionRepository $subscriptionRepository;
    /**
     * @var CancelSubscriptionService
     */
    private CancelSubscriptionService $cancelSubscriptionService;

    public function __construct(
        CreateSubscriptionService $createService,
        CancelSubscriptionService $cancelSubscriptionService,
        SubscriptionRepository $subscriptionRepository
    ) {
        $this->createSubscriptionService = $createService;
        $this->subscriptionRepository = $subscriptionRepository;
        $this->cancelSubscriptionService = $cancelSubscriptionService;
    }

    /**
     * @param string $email
     * @return Result
     */
    public function createSubscription(string $email)
    {
       $this->createSubscriptionService->create($email, Status::inactive());
       return Result::success();
    }

    /**
     * @param UUID $uuid
     * @return object|null
     */
    public function findSubscription(UUID $uuid): ?Subscription
    {
        return $this->subscriptionRepository->findByUUID($uuid);
    }

    /**
     * @param string $uuid
     * @return Result
     */
    public function cancelSubscription(string $uuid): Result
    {
        return $this->cancelSubscriptionService->cancelSubscription(new UUID($uuid));
    }
}