<?php
/**
 * @category    Fixit-ISI
 * @date        10/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Subscription;

use Doctrine\ORM\Mapping as ORM;

/**
 * Class Name
 * @package App\Domain\Subscription
 * @ORM\Embeddable
 */
class Name
{
    /**
     * @var string
     * @ORM\Column(name="name", type="string")
     */
    private string $value;

    public function __construct(string $value)
    {
        if ($value == "") {
            throw new \InvalidArgumentException('Invalid subscription name');
        }

        $this->value = $value;
    }

    public function __toString(): string
    {
        return $this->value;
    }

    public function isEqual(self $name): bool
    {
        return $this->value === $name->value;
    }
}