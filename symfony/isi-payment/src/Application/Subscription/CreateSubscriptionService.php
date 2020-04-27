<?php

namespace App\Application\Subscription;

use App\Common\Email;
use App\Domain\Subscription\Status;
use App\Domain\Subscription\Subscription;
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
     * CreateNewSubscriptionService constructor.
     * @param SubscriptionRepository $repository
     * @param EntityManagerInterface $entityManager
     */
    public function __construct(SubscriptionRepository $repository, EntityManagerInterface $entityManager)
    {
        $this->repository = $repository;
        $this->entityManager = $entityManager;
    }

    public function create(Email $email, Status $status): Subscription
    {
        $subscription = Subscription::subscription(
            $status,
            $email
        );
        $this->entityManager->persist($subscription);
        $this->entityManager->flush();
        return $subscription;
    }
}