<?php
namespace App\Application\Subscription;

use App\Common\Result;
use App\Common\UUID;
use App\Domain\Subscription\Subscription;
use App\Infrastructure\SubscriptionRepository;
use Doctrine\ORM\EntityManagerInterface;


final class CancelSubscriptionService
{
    /**
     * @var SubscriptionRepository
     */
    private SubscriptionRepository $subscriptionRepository;
    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $entityManager;

    public function __construct(SubscriptionRepository $subscriptionRepository, EntityManagerInterface $entityManager)
    {
        $this->subscriptionRepository = $subscriptionRepository;
        $this->entityManager = $entityManager;
    }

    /**
     * @param UUID $uuid
     * @return Result
     */
    public function cancelSubscription(UUID $uuid): Result
    {
        $subscription = $this->subscriptionRepository->findByUUID($uuid);
        if (!$subscription instanceof Subscription) {
            return Result::failure(sprintf('Subscription with UUID \'%s\' was not found', (string)$uuid), 404);
        }
        $result = $subscription->disable();
        if (!$result instanceof Result\Success) {
            return $result;
        }
        $this->entityManager->persist($subscription);
        $this->entityManager->flush();
        return Result::success();
    }
}