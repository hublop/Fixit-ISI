<?php

namespace App\Common;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Email
 * @package App\Common
 * @ORM\Embeddable
 */
final class Email
{
    /**
     * @var string
     * @ORM\Column(type="string", name="email")
     */
    private string $value;

    public function __construct(string $value)
    {
        if (!\preg_match("/^([a-z0-9\+_\-]+)(\.[a-z0-9\+_\-]+)*@([a-z0-9\-]+\.)+[a-z]{2,6}$/ix", $value)) {
            throw new \InvalidArgumentException('Invalid Email format');
        }

        $this->value = $value;
    }

    public function __toString(): string
    {
        return $this->value;
    }

    public function isEqual(self $email): bool
    {
        return $this->value === $email->value;
    }
}