<?php

namespace App\Application\Order;

use App\Common\Email;
use App\Common\Firstname;
use App\Common\Lastname;
use App\Domain\Order\Order;
use App\Domain\Order\OrderValue;
use App\Domain\Order\Status;
use App\Domain\Subscription\Subscription;
use App\Infrastructure\Doctrine\OrderRepository;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Contracts\EventDispatcher\EventDispatcherInterface;

class CreateOrderService
{
    private OrderRepository $orderRepository;
    private EntityManagerInterface $entityManager;
    private EventDispatcherInterface $eventDispatcher;

    public function __construct(
        OrderRepository $orderRepository,
        EntityManagerInterface $entityManager,
        EventDispatcherInterface $eventDispatcher
    ) {
        $this->orderRepository     = $orderRepository;
        $this->entityManager       = $entityManager;
        $this->eventDispatcher     = $eventDispatcher;
    }

    public function createOrderFromSubscription(
        Subscription $subscription
    ): Order {
        return Order::order(
            $subscription->getFirstname(),
            $subscription->getLastname(),
            $subscription->getEmail(),
            new OrderValue($subscription->getCost()->getValue(), $subscription->getCost()->getCurrencyCode()),
            Status::processing(),
            null,
            $subscription
        );
    }

    public function createRecurringOrderFromSubscription(
        Subscription $subscription
    ): Order {
        $order = $this->createOrderFromSubscription($subscription);
        $this->entityManager->persist($order);
        $this->entityManager->flush();
        return $order;
    }

}