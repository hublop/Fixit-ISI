<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Action\Payment;

use App\Action\Payment\Type\PaymentType;
use App\Application\Payment\PaymentFacade;
use App\Responder\JsonResponder;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class ProcessAction
 * @package App\Action\Payment
 * @Route("/payment/process", methods={"POST"}, name="payment_process")
 */
class ProcessAction
{
    /**
     * @var PaymentFacade
     */
    private PaymentFacade $domain;
    /**
     * @var JsonResponder
     */
    private JsonResponder $responder;

    public function __construct(PaymentFacade $domain, JsonResponder $responder)
    {
        $this->domain = $domain;
        $this->responder = $responder;
    }

    public function __invoke(PaymentType $paymentType)
    {
        return $this->responder->respond($this->domain->processPayment($paymentType->orderId, $paymentType->ip, $paymentType->paymentToken));
    }
}
