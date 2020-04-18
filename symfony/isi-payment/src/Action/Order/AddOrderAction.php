<?php

namespace App\Action\Order;

use App\Action\Order\Type\CreateOrderType;
use App\Application\Order\OrderFacade;
use App\Responder\JsonResponder;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class AddOrderAction
 * @package App\Action
 * @Route("/order", methods={"POST"}, name="add_order")
 */
class AddOrderAction
{
    /**
     * @var OrderFacade
     */
    private OrderFacade $domain;
    /**
     * @var JsonResponder
     */
    private JsonResponder $responder;

    public function __construct(OrderFacade $domain, JsonResponder $responder)
    {
        $this->domain = $domain;
        $this->responder = $responder;
    }

    public function __invoke(CreateOrderType $createOrderQuery)
    {
        return $this->responder->respond($this->domain->createOrder(
            $createOrderQuery->userId,
            $createOrderQuery->userEmail,
            $createOrderQuery->firstname,
            $createOrderQuery->lastname,
            $createOrderQuery->totalAmount
        ));
    }
}