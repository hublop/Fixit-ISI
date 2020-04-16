<?php

namespace App\Domain\Subscription;

use App\Common\UUID;
use Doctrine\ORM\Mapping as ORM;

/**
 * @ORM\Embeddable()
 */
final class SubscriptionId
{
    /**
     * @ORM\Embedded(class="App\Common\UUID", columnPrefix=false)
     */
    private UUID $id;

    public function __construct(UUID $id)
    {
        $this->id = $id;
    }

    public static function newOne(): self
    {
        return new self(UUID::random());
    }

    public function isEqual(self $id): bool
    {
        return $this->id->isEqual($id->id);
    }

    public function __toString()
    {
        return (string) $this->id;
    }
}