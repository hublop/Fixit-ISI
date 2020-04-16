<?php

namespace App\Domain\Order;

use App\Common\ExternalUUID;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class UserId
 * @package App\Domain\Order
 * @ORM\Embeddable
 */
class UserId
{
    /**
     * @ORM\Embedded(class="App\Common\ExternalUUID", columnPrefix="user")
     */
    private ExternalUUID $id;

    public function __construct(ExternalUUID $id)
    {
        $this->id = $id;
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