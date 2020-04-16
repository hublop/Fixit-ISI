<?php

namespace App\Domain\Order;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class OrderValue
 * @package App\Domain\Order
 * @ORM\Embeddable
 */
class OrderValue
{
    /**
     * @var int
     * @ORM\Column(name="orderValue", type="integer")
     */
    private int $value;

    public function __construct(int $value)
    {
        if ($value < 0) {
            throw new \InvalidArgumentException('Order value cannot be less than 0');
        }

        $this->value = $value;
    }

    public function __toString(): string
    {
        return $this->value;
    }

    public function isEqual(self $value): bool
    {
        return $this->value === $value->value;
    }
}