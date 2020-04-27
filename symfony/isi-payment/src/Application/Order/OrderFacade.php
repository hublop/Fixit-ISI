<?php

namespace App\Application\Order;

use App\Application\Subscription\CreateSubscriptionService;
use App\Common\Email;
use App\Common\Result;
use App\Common\Url;
use App\Common\UUID;
use App\Domain\Order;
use App\Domain\Payment\LanguageCode;
use App\Domain\Payment\MerchantPosId;
use App\Domain\Payment\PaymentWidget;
use App\Domain\Payment\ShopName;
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
    private array $configuration;

    public function __construct(
        CreateOrderService $createOrderService,
        OrderRepository $orderRepository,
        CreateSubscriptionService $createSubscriptionService,
        EntityManagerInterface $entityManager,
        array $payuConfig
    ) {
        $this->createOrderService = $createOrderService;
        $this->orderRepository = $orderRepository;
        $this->createSubscriptionService = $createSubscriptionService;
        $this->em = $entityManager;
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
        $this->em->beginTransaction();
        try {
            $subscription = $this->createSubscriptionService->create(
                $userEmail,
                Subscription\Status::inactive()
            );
            $order = $this->createOrderService->create(
                $userId,
                $firstname,
                $lastname,
                $totalAmount,
                $subscription,
                Order\Status::processing()
            );
            $this->em->commit();
            $widget = $this->getPaymentWidget($order);
        } catch (\Exception $exception) {
            $this->em->rollback();
            return Result::failure($exception->getMessage(), 400);
        }

        return Result::success([new Order\OrderCreated(
            $order->getId(),
            $order->getSubscription()->id(),
            new Url($this->configuration['url']),
            $widget
        )]);
    }

    public function getPaymentWidget(Order\Order $order): PaymentWidget
    {
        return PaymentWidget::fromOrder(
            $order,
            new MerchantPosId($this->configuration['posId']),
            new ShopName($this->configuration['shopName']),
            LanguageCode::pl(),
            true,
            true,
            $this->configuration['privateKey']
        );
    }
}