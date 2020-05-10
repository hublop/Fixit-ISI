<?php

namespace App\Application\Subscription;

use App\Application\Payment\WidgetService;
use App\Common\Email;
use App\Common\Firstname;
use App\Common\Lastname;
use App\Common\Result;
use App\Common\Url;
use App\Common\UUID;
use App\Domain\Subscription\Cost;
use App\Domain\Subscription\Name;
use App\Domain\Subscription\Status;
use App\Domain\Subscription\Subscription;
use App\Domain\Subscription\SubscriptionCreated;
use App\Domain\Subscription\UserId;
use App\Infrastructure\Doctrine\SubscriptionRepository;

final class SubscriptionFacade
{
    private CreateSubscriptionService $createSubscriptionService;
    private SubscriptionRepository $subscriptionRepository;
    private CancelSubscriptionService $cancelSubscriptionService;
    private WidgetService $paymentWidgetService;
    private array $configuration;
    private DisableSubscriptionService $disableService;

    public function __construct(
        CreateSubscriptionService $createService,
        CancelSubscriptionService $cancelSubscriptionService,
        DisableSubscriptionService $disableService,
        SubscriptionRepository $subscriptionRepository,
        WidgetService $widgetService,
        array $payuConfig
    ) {
        $this->createSubscriptionService = $createService;
        $this->subscriptionRepository = $subscriptionRepository;
        $this->disableService = $disableService;
        $this->cancelSubscriptionService = $cancelSubscriptionService;
        $this->paymentWidgetService = $widgetService;
        $this->configuration = $payuConfig;
    }

    public function createSubscription(
        Name $subscriptionName,
        UserId $userId,
        Email $userEmail,
        Firstname $firstname,
        Lastname $lastname,
        Cost $cost
    ): Result {
       $result = $this->createSubscriptionService->create(
           $subscriptionName,
           $userId,
           $userEmail,
           $firstname,
           $lastname,
           $cost
       );
       if ($result->isFailure()) {
           return $result;
       }
       /** @var SubscriptionCreated[] $events */
       $events = $result->events();
       $order = reset($events)->order;
       $subscription = reset($events)->subscription;
       $widget = $this->paymentWidgetService->getPaymentWidget(reset($events)->order);

       return Result::success([
           new SubscriptionPaymentCreated(
               $order->getId(),
               $subscription->getUserId(),
               $order->getOrderValue(),
               $subscription->id(),
               new Url($this->configuration['url']),
               $widget
           )
       ]);
    }

    public function findSubscription(UUID $uuid): ?Subscription
    {
        return $this->subscriptionRepository->findByUUID($uuid);
    }

    public function cancelSubscription(string $uuid): Result
    {
        return $this->cancelSubscriptionService->cancelSubscription(new UUID($uuid));
    }

    public function findSubscriptionToReccure(\DateTimeImmutable $dateTimeImmutable)
    {
        return $this->subscriptionRepository->findByDateStatuses($dateTimeImmutable, [(string) Status::active(), (string) Status::cancelled()]);
    }

    public function disableSubscription(Subscription $subscription): Result
    {
        return $this->disableService->disableSubscription($subscription);
    }
}