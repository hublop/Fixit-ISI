<?php

namespace App\Action\Subscription;

use App\Application\Subscription\SubscriptionFacade;
use App\Common\UUID;
use App\Responder\JsonResponder;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class GetSubscriptionAction
 * @package App\Action\Subscription
 * @Route("/subscription/{id}", name="get_subscription")
 */
class GetSubscriptionAction
{
    /**
     * @var SubscriptionFacade
     */
    private SubscriptionFacade $domain;
    /**
     * @var JsonResponder
     */
    private JsonResponder $responder;

    public function __construct(SubscriptionFacade $facade, JsonResponder $responder)
    {
        $this->domain = $facade;
        $this->responder = $responder;
    }

    public function __invoke(string $id)
    {
        return $this->responder->respond($this->domain->findSubscription(new UUID($id)));
    }
}
