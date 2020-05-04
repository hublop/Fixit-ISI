<?php

namespace App\Application\Order;

use App\Application\Payment\WidgetService;
use App\Application\Subscription\CreateSubscriptionService;
use App\Common\Email;
use App\Common\Result;
use App\Common\Url;
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
     * @var \App\Infrastructure\Doctrine\OrderRepository
     */
    private OrderRepository $orderRepository;
    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $em;
    /**
     * @var CreateSubscriptionService
     */
    private CreateSubscriptionService $createSubscriptionService;
    /**
     * @var WidgetService
     */
    private WidgetService $widgetService;
    private array $configuration;

    public function __construct(
        CreateOrderService $createOrderService,
        OrderRepository $orderRepository,
        CreateSubscriptionService $createSubscriptionService,
        EntityManagerInterface $entityManager,
        WidgetService $widgetService,
        array $payuConfig
    ) {
        $this->createOrderService        = $createOrderService;
        $this->orderRepository           = $orderRepository;
        $this->createSubscriptionService = $createSubscriptionService;
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

    public function createOrder(
        Order\UserId $userId,
        Email $userEmail,
        Order\Firstname $firstname,
        Order\Lastname $lastname,
        Order\OrderValue $totalAmount
    ) {
        try {
            $order = $this->createOrderService->create(
                $userId,
                $userEmail,
                $firstname,
                $lastname,
                $totalAmount,
                Subscription\Status::inactive()
            );
            $widget = $this->widgetService->getPaymentWidget($order);

        } catch (\Exception $exception) {
            $this->em->rollback();
            return Result::failure($exception->getMessage(), 400);
        }
        return Result::success([
            new Order\OrderCreated(
                $order->getId(),
                $order->getSubscription()->id(),
                new Url($this->configuration['url']),
                $widget
            )
        ]);
    }

    /**
     * @param Subscription\Subscription $subscription
     * @return Order\Order
     */
    public function getLastSubscriptionOrder(Subscription\Subscription $subscription)
    {
        return $this->orderRepository->findOneBy(['subscription' => $subscription->id()], ['createdAt' => 'DESC']);
    }

    public function createRucurringOrder(Order\Order $oldOrder): Order\Order
    {
        return $this->createOrderService->createRecurredOrder($oldOrder);
    }
}