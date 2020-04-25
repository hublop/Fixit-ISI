<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use App\Common\UUID;
use Doctrine\ORM\Mapping as ORM;

/**
* @ORM\Embeddable()
*/
class PaymentId
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

    /**
     * @return UUID
     */
    public function getId(): UUID
    {
        return $this->id;
    }
}
