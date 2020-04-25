<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use App\Domain\Order\Order;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Payment
 * @package App\Domain\Payment
 * @ORM\Entity(repositoryClass="App\Infrastructure\Doctrine\PaymentRepository")
 */
class Payment
{
    /**
     * @var PaymentId
     * @ORM\Embedded(class="App\Domain\Payment\PaymentId", columnPrefix=false )
     */
    private PaymentId $id;
    /**
     * @var Status
     * @ORM\Embedded(class="App\Domain\Payment\Status", columnPrefix=false)
     */
    private Status $status;

    /**
     * @var string
     * @ORM\Column(type="string", name="cardNumber")
     */
    private string $cardNumber;
    /**
     * @var PaymentToken
     * @ORM\Embedded(class="App\Domain\Payment\PaymentToken", columnPrefix=false)
     */
    private ?PaymentToken $token;

    /**
     * @var Order
     * @ORM\ManyToOne(targetEntity="App\Domain\Order\Order")
     * @ORM\JoinColumn(name="order_id", referencedColumnName="id")
     */
    private Order $order;

    public function __construct(PaymentId $id, Status $status, string $cardNumber, Order $order, ?PaymentToken $token)
    {
        $this->id         = $id;
        $this->status     = $status;
        $this->cardNumber = $cardNumber;
        $this->order      = $order;
        $this->token = $token;
    }

    public static function payment(Status $status, string $cardNumber, Order $order, ?PaymentToken $token): self
    {
        return new self(
            PaymentId::newOne(),
            $status,
            $cardNumber,
            $order,
            $token
        );
    }
}