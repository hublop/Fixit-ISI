<?php

namespace App\Application\Order;

use App\Application\Subscription\CreateSubscriptionService;
use App\Common\Email;
use App\Domain\Order\Firstname;
use App\Domain\Order\Lastname;
use App\Domain\Order\Order;
use App\Domain\Order\OrderValue;
use App\Domain\Order\Status;
use App\Domain\Order\UserId;
use App\Domain\Subscription;
use App\Infrastructure\Doctrine\OrderRepository;
use Doctrine\ORM\EntityManagerInterface;

class CreateOrderService
{
    private OrderRepository $orderRepository;
    private EntityManagerInterface $entityManager;
    private CreateSubscriptionService $subscriptionService;

    public function __construct(
        OrderRepository $orderRepository,
        EntityManagerInterface $entityManager,
        CreateSubscriptionService $subscriptionFacade
    ) {
        $this->orderRepository = $orderRepository;
        $this->entityManager = $entityManager;
        $this->subscriptionService = $subscriptionFacade;
    }

    public function create(
        UserId $userId,
        Email $email,
        Firstname $firstname,
        Lastname $lastname,
        OrderValue $orderValue,
        Subscription\Status $status
    ): Order {
        $subscription = $this->subscriptionService->create(
            $email,
            $status
        );
        $order        = Order::order($userId, $firstname, $lastname, $orderValue, Status::processing(), null, $subscription);
        $this->entityManager->persist($subscription);
        $this->entityManager->persist($order);
        $this->entityManager->flush();
        return $order;
    }

    public function createRecurredOrder(Order $oldOrder): Order
    {
        $order = Order::createRecurredOrder($oldOrder);
        $this->entityManager->persist($order);
        $this->entityManager->flush();
        return $order;
    }

}