<?php

namespace App\Action\Order;

use Symfony\Component\Routing\Annotation\Route;

/**
 * Class GetOrderAction
 * @package App\Action
 * @Route("/order/{id}", name="get_order")
 */
class GetOrderAction
{
    public function __invoke(int $id)
    {

    }
}