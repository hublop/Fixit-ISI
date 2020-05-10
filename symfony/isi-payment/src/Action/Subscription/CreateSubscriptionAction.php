<?php
/**
 * @category    Fixit-ISI
 * @date        10/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Action\Subscription;

use App\Application\Subscription\SubscriptionFacade;
use App\Responder\JsonResponder;
use App\Action\Subscription\Type\CreateSubscriptionType;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class CreateSubscriptionAction
 * @package App\Action\Subscription
 * @Route("/subscription", methods={"POST"})
 */
class CreateSubscriptionAction
{
    private SubscriptionFacade $domain;
    private JsonResponder $responder;

    public function __construct(SubscriptionFacade $domain, JsonResponder $responder)
    {
        $this->domain = $domain;
        $this->responder = $responder;
    }

    public function __invoke(CreateSubscriptionType $createOrderQuery)
    {
        return $this->responder->respond($this->domain->createSubscription(
            $createOrderQuery->name,
            $createOrderQuery->userId,
            $createOrderQuery->userEmail,
            $createOrderQuery->firstname,
            $createOrderQuery->lastname,
            $createOrderQuery->cost
        ));
    }
}