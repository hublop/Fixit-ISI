<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Application\Payment;

use App\Application\Order\OrderFacade;
use App\Common\IP;
use App\Common\Result;
use App\Domain\Order\OrderId;
use App\Domain\Payment\Gateway\GatewayInterface;
use App\Domain\Payment\Payment;
use App\Domain\Payment\PaymentToken;
use App\Domain\Payment\Status;
use Doctrine\ORM\EntityManagerInterface;

class ProcessPaymentService
{
    private array $configuration;
    /**
     * @var OrderFacade
     */
    private OrderFacade $orderService;
    /**
     * @var GatewayInterface
     */
    private GatewayInterface $paymentGateway;

    /**
     * @var EntityManagerInterface
     */
    private EntityManagerInterface $entityManager;

    public function __construct(
        OrderFacade $orderService,
        array $payuConfig,
        GatewayInterface $paymentGateway,
        EntityManagerInterface $entityManager
    ) {
        $this->paymentGateway = $paymentGateway;
        $this->configuration  = $payuConfig;
        $this->orderService   = $orderService;
        $this->entityManager  = $entityManager;
    }

    public function processPayment(
        OrderId $orderId,
        IP $customerIP,
        PaymentToken $paymentToken,
        bool $isFirst = true
    ): Result {
        $order = $this->orderService->findOrder($orderId->getId());
        $subscription = $order->getSubscription();
        if (!$order) {
            return Result::failure('Could not find order with given ID', 404);
        }
        $orderRequest = [
            'notifyUrl'     => $this->configuration['notifyUrl'],
            'customerIp'    => (string)$customerIP,
            'merchantPosId' => $this->configuration['posId'],
            'recurring'     => $isFirst ? 'FIRST' : 'STANDARD',
            'description'   => $this->configuration['orderDescription'],
            'currencyCode'  => $order->getOrderValue()->getCurrencyCode(),
            'totalAmount'   => $order->getOrderValue()->getValue(),
            'extOrderId'    => (string)$orderId,
            'products'      => [
                [
                    'name'      => $this->configuration['orderProduct'],
                    'unitPrice' => $order->getOrderValue()->getValue(),
                    'quantity'  => 1
                ]
            ],
            'buyer'         => [
                'email' => (string)$order->getSubscription()->getEmail(),
            ],
            'payMethods'    => [
                'payMethod' => [
                    'value' => (string)$paymentToken,
                    'type'  => 'CARD_TOKEN'
                ]
            ]
        ];
        $result     = $this->paymentGateway->create($orderRequest, $isFirst);
        $payment      = Payment::payment($result->getStatus(), $result->getCardNumber(), $result->getToken());
        if ($result->isSuccessfull()) {
            $order->markSucceded();
            $subscription->activate();
        }
        $order->addPayment($payment);
        $this->entityManager->persist($payment);
        $this->entityManager->persist($order);
        $this->entityManager->persist($subscription);
        $this->entityManager->flush();
        if ($result->getStatus() == Status::success) {
            return Result::success();
        } else {
            return Result::failure($result->getMessage(), 400);
        }
    }
}