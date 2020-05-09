<?php
/**
 * @category    Fixit-ISI
 * @date        07/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */
namespace App\Infrastructure\Messenger\Order;

use App\Domain\Order\OrderCreated;
use Symfony\Component\EventDispatcher\EventSubscriberInterface;
use Symfony\Component\Messenger\MessageBusInterface;

class OrderCreatedEventSubscriber implements EventSubscriberInterface
{
    private MessageBusInterface $bus;

    public function __construct(MessageBusInterface $messageBus)
    {
        $this->bus = $messageBus;
    }

    public static function getSubscribedEvents()
    {
       return [
           OrderCreated::class => 'sendNotification'
       ];
    }

    public function sendNotification(OrderCreated $orderCreated)
    {
        $this->bus->dispatch($orderCreated);
    }
}