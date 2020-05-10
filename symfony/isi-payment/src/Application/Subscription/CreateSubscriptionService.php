<?php

namespace App\Application\Subscription;

use App\Application\Order\CreateOrderService;
use App\Common\Email;
use App\Common\Firstname;
use App\Common\Lastname;
use App\Common\Result;
use App\Domain\Subscription\Cost;
use App\Domain\Subscription\Name;
use App\Domain\Subscription\Subscription;
use App\Domain\Subscription\SubscriptionCreated;
use App\Domain\Subscription\UserId;
use App\Infrastructure\Doctrine\SubscriptionRepository;
use Doctrine\ORM\EntityManagerInterface;

class CreateSubscriptionService
{
    /**
     * @var \App\Infrastructure\Doctrine\SubscriptionRepository
     */
    private SubscriptionRepository $repository;
    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $entityManager;
    /**
     * @var CreateOrderService
     */
    private CreateOrderService $orderService;

    /**
     * CreateNewSubscriptionService constructor.
     * @param SubscriptionRepository $repository
     * @param EntityManagerInterface $entityManager
     * @param CreateOrderService     $orderService
     */
    public function __construct(SubscriptionRepository $repository, EntityManagerInterface $entityManager, CreateOrderService $orderService)
    {
        $this->repository = $repository;
        $this->entityManager = $entityManager;
        $this->orderService = $orderService;
    }

    /**
     * @param Name                  $subscriptionName
     * @param UserId                $userId
     * @param Email                 $userEmail
     * @param \App\Common\Firstname $firstname
     * @param \App\Common\Lastname  $lastname
     * @param Cost                  $cost
     * @return Subscription
     */
    public function create(
        Name $subscriptionName,
        UserId $userId,
        Email $userEmail,
        Firstname $firstname,
        Lastname $lastname,
        Cost $cost
    ): Result {
        $existingSubscriptions = $this->repository->getActiveUserSubscriptions($subscriptionName, $userId);
        if (count($existingSubscriptions) > 0) {
            return Result::failure('User have an active subscription');
        }
        $subscription = Subscription::newSubscription(
            $subscriptionName,
            $userId,
            $cost,
            $userEmail,
            $firstname,
            $lastname
        );
        $order = $this->orderService->createOrderFromSubscription($subscription);
        try {
            $this->entityManager->persist($subscription);
            $this->entityManager->persist($order);
            $this->entityManager->flush();
        } catch (\Exception $exception) {
            return Result::failure($exception->getMessage());
        }
        return Result::success([new SubscriptionCreated($subscription, $order)]);
    }
}