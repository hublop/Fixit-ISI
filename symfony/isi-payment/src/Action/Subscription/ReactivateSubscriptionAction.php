<?php

namespace App\Action\Subscription;

use App\Application\Subscription\SubscriptionFacade;
use App\Responder\JsonResponder;
use Symfony\Component\Routing\Annotation\Route;

/**
 * Class AddSubscriptionAction
 * @package App\Action\Subscription
 * @Route("/subscription/{uuid}/reactivate", methods={"POST"}, name="reactivate_subscription")
 */
class ReactivateSubscriptionAction
{
    /**
     * @var SubscriptionFacade
     */
    private SubscriptionFacade $domain;
    /**
     * @var JsonResponder
     */
    private JsonResponder $responder;

    public function __construct(SubscriptionFacade $domain, JsonResponder $responder)
    {
        $this->domain = $domain;
        $this->responder = $responder;
    }

    /**
     * @param string $uuid
     * @return \Symfony\Component\HttpFoundation\JsonResponse
     */
    public function __invoke(string $uuid)
    {
        return $this->responder->respond($this->domain->reactivateSubscription($uuid));
    }
}
