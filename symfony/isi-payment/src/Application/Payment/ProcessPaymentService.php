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
    )
    {
        $this->paymentGateway = $paymentGateway;
        $this->configuration = $payuConfig;
        $this->orderService = $orderService;
        $this->entityManager = $entityManager;
    }

    public function processPayment(OrderId $orderId, IP $customerIP, PaymentToken $paymentToken): Result
    {
        $order = $this->orderService->findOrder($orderId->getId());
        if (!$order) {
            return Result::failure('Could not find order with given ID', 404);
        }
        $orderRequest = [
            'notifyUrl' => $this->configuration['notifyUrl'],
            'customerIp' => (string) $customerIP,
            'merchantPosId' => $this->configuration['posId'],
            'recurring' => 'STANDARD',
            'description' => $this->configuration['orderDescription'],
            'currencyCode' => $order->getOrderValue()->getCurrencyCode(),
            'totalAmount' => $order->getOrderValue()->getValue(),
            'extOrderId' => (string) $orderId,
            'products' => [
                [
                    'name' => $this->configuration['orderProduct'],
                    'unitPrice' => $order->getOrderValue()->getValue(),
                    'quantity' => 1
                ]
            ],
            'buyer' => [
                'email' => (string) $order->getSubscription()->getEmail(),
            ],
            'payMethods' => [
                'payMethod' => [
                    'value' => (string) $paymentToken,
                    'type' => 'CARD_TOKEN'
                ]
            ]
        ];
        $response = $this->paymentGateway->create($orderRequest);
//        $payment = Payment::payment($response->getStatus(), $response->getCardNumber(), $response->getToken());
        $payment = Payment::payment($response->getStatus(), $response->getCardNumber(), new PaymentToken("TOKC_KPNZVSLJUNR4DHF5NPVKDPJGMX7"));
        $order->addPayment($payment);
        $this->entityManager->persist($payment);
        $this->entityManager->persist($order);
        $this->entityManager->flush();
        if ($response->getStatus() == Status::success) {
            return Result::success();
        } else {
            return Result::failure($response->getMessage(), 400);
        }
    }
}