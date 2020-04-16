<?php

namespace App\Common;

use App\Common\Result\Failure;
use App\Common\Result\Success;

abstract class Result implements \JsonSerializable
{
    public static function success(array $events = []): Success
    {
        return new Success($events);
    }

    public static function failure(string $reason, $code = 0): Failure
    {
        return new Failure($reason, $code);
    }

    public function isFailure(): bool
    {
        return $this instanceof Failure;
    }

    public function isSuccessful(): bool
    {
        return $this instanceof Success;
    }

    public function reason(): string
    {
        if ($this instanceof Failure) {
            return $this->reason;
        }

        return 'OK';
    }

    /**
     * @return DomainEvent[]
     */
    public function events(): array
    {
        if ($this instanceof Success) {
            return $this->events;
        }

        return [];
    }
}