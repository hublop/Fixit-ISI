<?php
/**
 * @category    symfony
 * @date        02/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Application\RecurringPayment;

use App\Application\Order\OrderFacade;
use App\Application\Payment\PaymentFacade;
use App\Application\Payment\ProcessPaymentService;
use App\Application\Subscription\SubscriptionFacade;
use App\Common\IP;
use App\Common\Result;
use App\Domain\Payment\PaymentToken;
use App\Domain\Subscription\Subscription;
use Doctrine\ORM\EntityManagerInterface;

class RecurringPaymentManager
{
    /**
     * @var SubscriptionFacade
     */
    private SubscriptionFacade $subscriptionService;
    /**
     * @var PaymentFacade
     */
    private OrderFacade $orderService;
    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $entityManager;
    /**
     * @var ProcessPaymentService
     */
    private ProcessPaymentService $paymentService;

    public function __construct(
        SubscriptionFacade $subscriptionFacade,
        OrderFacade $orderFacade,
        ProcessPaymentService $paymentService,
        EntityManagerInterface $entityManager
    ) {
        $this->subscriptionService = $subscriptionFacade;
        $this->orderService        = $orderFacade;
        $this->entityManager       = $entityManager;
        $this->paymentService      = $paymentService;
    }

    public function processPayments(\DateTimeImmutable $dateTimeImmutable): \Iterator
    {
        /** @var Subscription[] $subscriptions */
        $subscriptions = $this->subscriptionService->findSubscriptionToReccure($dateTimeImmutable);
        foreach ($subscriptions as $subscription) {
            $oldOrder = $this->orderService->getLastSubscriptionOrder($subscription);
            $payment  = $oldOrder->getPayment();
            $order    = $this->orderService->createRucurringOrder($oldOrder);
            if (!$payment->getToken() instanceof PaymentToken) {
                yield Result::failure(sprintf(
                    "Could not process payment for subscription %s: missing payment token",
                    (string)$subscription->id()
                ));
            }
            yield $this->paymentService->processPayment($order->getId(), IP::localIP(), $payment->getToken());
        }
    }
}