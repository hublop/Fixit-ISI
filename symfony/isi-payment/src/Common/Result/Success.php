<?php

namespace App\Common\Result;

use App\Common\DomainEvent;
use App\Common\Result;

final class Success extends Result
{
    /**
     * @var DomainEvent[]
     */
    protected array $events;

    /**
     * @param DomainEvent[] $events
     */
    public function __construct(array $events)
    {
        $this->events = array_map(function (DomainEvent $event): DomainEvent {return  $event; }, $events);
    }

    /**
     * @return array
     */
    public function jsonSerialize()
    {
        $returnValue =  [
            'success' => true
        ];
        if (count($this->events) == 1) {
            /** @var DomainEvent $event */
            $event = reset($this->events);
            $returnValue = array_merge($returnValue, $event->jsonSerialize());
        }
        return $returnValue;
    }
}