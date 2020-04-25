<?php

namespace App\Application\Order;

use App\Domain\Order\Firstname;
use App\Domain\Order\Lastname;
use App\Domain\Order\Order;
use App\Domain\Order\OrderValue;
use App\Domain\Order\Status;
use App\Domain\Order\UserId;
use App\Domain\Subscription\Subscription;
use App\Infrastructure\Doctrine\OrderRepository;
use Doctrine\ORM\EntityManagerInterface;

class CreateOrderService
{
    /**
     * @var \App\Infrastructure\Doctrine\OrderRepository
     */
    private OrderRepository $orderRepository;
    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $entityManager;

    public function __construct(
        OrderRepository $orderRepository,
        EntityManagerInterface $entityManager
    )
    {
        $this->orderRepository = $orderRepository;
        $this->entityManager = $entityManager;
    }

    public function create(UserId $userId, Firstname $firstname, Lastname $lastname, OrderValue $orderValue, Subscription $subscription, Status $status): Order
    {
        $order = Order::order($userId, $firstname, $lastname, $orderValue, $status, $subscription);
        $this->entityManager->persist($order);
        $this->entityManager->flush();
        return $order;
    }
}