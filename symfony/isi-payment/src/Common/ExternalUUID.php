<?php

namespace App\Common;

use Ramsey\Uuid\Uuid as RamseyUuid;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class UUID
 * @package App\Common
 * @ORM\Embeddable
 */
final class ExternalUUID
{
    /**
     * @var string
     * @ORM\Column(type="uuid", name="id")
     */
    private string $value;

    public function __construct(string $value)
    {
        if (!\preg_match('/^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$/', $value)) {
            throw new \InvalidArgumentException('Invalid UUID format');
        }

        $this->value = $value;
    }

    public function __toString(): string
    {
        return $this->value;
    }

    public function isEqual(self $id): bool
    {
        return $this->value === $id->value;
    }
}