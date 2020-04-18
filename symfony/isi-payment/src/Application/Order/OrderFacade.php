<?php

namespace App\Application\Order;

use App\Application\Subscription\CreateSubscriptionService;
use App\Common\Email;
use App\Domain\Order;
use App\Common\Result;
use App\Common\UUID;
use App\Domain\Subscription;
use App\Infrastructure\OrderRepository;
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
     * @var CreateSubscriptionService
     */
    private CreateSubscriptionService $createSubscriptionService;

    public function __construct(
        CreateOrderService $createOrderService,
        OrderRepository $orderRepository,
        CreateSubscriptionService $createSubscriptionService,
        EntityManagerInterface $entityManager
    ) {
        $this->createOrderService = $createOrderService;
        $this->orderRepository = $orderRepository;
        $this->createSubscriptionService = $createSubscriptionService;
        $this->em = $entityManager;
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
            $this->createOrderService->create(
                $userId,
                $firstname,
                $lastname,
                $totalAmount,
                $subscription,
                Order\Status::processing()
            );
            $this->em->commit();
        } catch (\Exception $exception) {
            $this->em->rollback();
            return Result::failure($exception->getMessage(), 400);
        }

        return Result::success();
    }
}