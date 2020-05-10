<?php
/**
 * @category    Fixit-ISI
 * @date        10/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Subscription;

use App\Common\CurrencyCode;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Cost
 * @package App\Domain\Subscription
 * @ORM\Embeddable
 */
class Cost
{
    /**
     * @var int
     * @ORM\Column(name="cost", type="integer")
     */
    private int $value;

    /**
     * @var CurrencyCode
     * @ORM\Embedded(class="App\Common\CurrencyCode", columnPrefix=false)
     */
    private CurrencyCode $currencyCode;

    public function __construct(int $value, CurrencyCode $currencyCode)
    {
        if ($value < 0) {
            throw new \InvalidArgumentException('Subscription cost cannot be less than 0');
        }

        $this->currencyCode  = $currencyCode;
        $this->value = $value;
    }

    public function __toString(): string
    {
        return sprintf("%s %s", (string) $this->currencyCode, $this->value);
    }

    public function isEqual(self $value): bool
    {
        return $this->value === $value->value && $this->currencyCode->equals($value->currencyCode);
    }

    /**
     * @return int
     */
    public function getValue(): int
    {
        return $this->value;
    }

    /**
     * @return CurrencyCode
     */
    public function getCurrencyCode(): CurrencyCode
    {
        return $this->currencyCode;
    }
}