<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Action\Order;

use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class ProcessAction
 * @package App\Action\Order
 * @Route("/order/process", name="process_order")
 */
class ProcessAction
{
    public function __invoke(Request $request)
    {
        die(var_dump($request));
    }
}