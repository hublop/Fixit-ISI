<?php

namespace App\Application\Order;

use App\Application\Payment\WidgetService;
use App\Common\UUID;
use App\Domain\Order;
use App\Domain\Subscription;
use App\Infrastructure\Doctrine\OrderRepository;
use Doctrine\ORM\EntityManagerInterface;

class OrderFacade
{
    /**
     * @var CreateOrderService
     */
    private CreateOrderService $createOrderService;
    /**
     * @var OrderRepository
     */
    private OrderRepository $orderRepository;
    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $em;
    /**
     * @var WidgetService
     */
    private WidgetService $widgetService;
    private array $configuration;

    public function __construct(
        CreateOrderService $createOrderService,
        OrderRepository $orderRepository,
        EntityManagerInterface $entityManager,
        WidgetService $widgetService,
        array $payuConfig
    ) {
        $this->createOrderService        = $createOrderService;
        $this->orderRepository           = $orderRepository;
        $this->em                        = $entityManager;
        $this->widgetService = $widgetService;
        $this->configuration = $payuConfig;
    }

    /**
     * @param UUID $uuid
     * @return \App\Domain\Order\Order|object|null
     */
    public function findOrder(UUID $uuid)
    {
        return $this->orderRepository->findByUUID($uuid);
    }

    /**
     * @param Subscription\Subscription $subscription
     * @return Order\Order
     */
    public function getLastSubscriptionOrder(Subscription\Subscription $subscription)
    {
        return $this->orderRepository->findOneBy(['subscription' => $subscription->id()], ['createdAt' => 'DESC']);
    }

    public function createRucurringOrder(Subscription\Subscription $oldOrder): Order\Order
    {
        return $this->createOrderService->createRecurringOrderFromSubscription($oldOrder);
    }
}