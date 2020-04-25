<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;


use Doctrine\ORM\Mapping as ORM;

/**
 * Class PaymentToken
 * @package App\Domain\Payment
 * @ORM\Embeddable
 */
class PaymentToken
{
    /**
     * @var string
     * @ORM\Column(name="token", type="string", nullable=true)
     */
    private $value;

    public function __construct(string $token)
    {
        if (!strpos($token, 'TOK') === 0) {
            throw new \InvalidArgumentException("Invalid payment token");
        }
        $this->value = $token;
    }

    public function __toString()
    {
        return $this->value;
    }
}