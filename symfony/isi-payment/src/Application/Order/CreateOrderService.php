<?php

namespace App\Application\Order;

use App\Application\Subscription\CreateSubscriptionService;
use App\Common\Email;
use App\Domain\Order\Firstname;
use App\Domain\Order\Lastname;
use App\Domain\Order\Order;
use App\Domain\Order\OrderValue;
use App\Domain\Order\Status;
use App\Domain\Subscription;
use App\Domain\Subscription\UserId;
use App\Infrastructure\Doctrine\OrderRepository;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Contracts\EventDispatcher\EventDispatcherInterface;

class CreateOrderService
{
    private OrderRepository $orderRepository;
    private EntityManagerInterface $entityManager;
    private CreateSubscriptionService $subscriptionService;
    private EventDispatcherInterface $eventDispatcher;

    public function __construct(
        OrderRepository $orderRepository,
        EntityManagerInterface $entityManager,
        CreateSubscriptionService $subscriptionFacade,
        EventDispatcherInterface $eventDispatcher
    ) {
        $this->orderRepository     = $orderRepository;
        $this->entityManager       = $entityManager;
        $this->subscriptionService = $subscriptionFacade;
        $this->eventDispatcher     = $eventDispatcher;
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
            $userId,
            $email,
            $status
        );
        $order        = Order::order($firstname, $lastname, $orderValue, Status::processing(), null,
            $subscription);
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