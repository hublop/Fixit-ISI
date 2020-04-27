<?php

namespace App\Action\Order;

use App\Action\Order\Type\GetOrderType;
use App\Application\Order\OrderFacade;
use App\Common\UUID;
use App\Responder\JsonResponder;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class GetOrderAction
 * @package App\Action
 * @Route("/order/{uuid}", name="get_order")
 */
class GetOrderAction
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

    public function __invoke(GetOrderType $getOrder)
    {
        return $this->responder->respond(
            $this->domain->findOrder($getOrder->uuid)
        );
    }
}
