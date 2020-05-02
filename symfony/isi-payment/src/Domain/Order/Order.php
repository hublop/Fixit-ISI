<?php

namespace App\Domain\Order;
use App\Common\Clock;
use App\Common\Result;
use App\Domain\Payment\Payment;
use App\Domain\Subscription\Subscription;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Order
 * @package App\Domain\Order
 * @ORM\Entity(repositoryClass="App\Infrastructure\Doctrine\OrderRepository")
 * @ORM\Table(name="subscription_order")
 */
class Order implements \JsonSerializable
{
    /**
     * @var OrderId
     * @ORM\Embedded(class="App\Domain\Order\OrderId", columnPrefix=false)
     */
    private OrderId $id;

    /**
     * @var UserId
     * @ORM\Embedded(class="App\Domain\Order\UserId", columnPrefix=false)
     */
    private UserId $userId;

    /**
     * @var Subscription
     * @ORM\ManyToOne(targetEntity="App\Domain\Subscription\Subscription")
     * @ORM\JoinColumn(name="subscription_id", referencedColumnName="id")
     */
    private Subscription $subscription;
    /**
     * @var Firstname
     * @ORM\Embedded(class="App\Domain\Order\Firstname", columnPrefix=false)
     */
    private Firstname $firstname;

    /**
     * @var Lastname
     * @ORM\Embedded(class="App\Domain\Order\Lastname", columnPrefix=false)
     */
    private Lastname $lastname;

    /**
     * @var OrderValue
     * @ORM\Embedded(class="App\Domain\Order\OrderValue", columnPrefix=false)
     */
    private OrderValue $orderValue;
    /**
     * @var Status
     * @ORM\Embedded(class="App\Domain\Order\Status", columnPrefix=false)
     */
    private Status $status;

    /**
     * @var Payment
     * @ORM\ManyToOne(targetEntity="App\Domain\Payment\Payment")
     * @ORM\JoinColumn(name="payment_id", referencedColumnName="id", nullable=true)
     */
    private ?Payment $payment;

    /**
     * @var \DateTimeImmutable
     * @ORM\Column(name="created_at", type="datetime_immutable")
     */
    private \DateTimeImmutable $createdAt;

    public function __construct(
        OrderId $id,
        UserId $userId,
        Firstname $firstname,
        Lastname $lastname,
        OrderValue $value,
        Status $status,
        Subscription $subscription,
        ?Payment $payment,
        \DateTimeImmutable $createdAt
    ) {
        $this->id = $id;
        $this->userId = $userId;
        $this->status = $status;
        $this->subscription = $subscription;
        $this->firstname = $firstname;
        $this->lastname = $lastname;
        $this->orderValue = $value;
        $this->payment = $payment;
        $this->createdAt = $createdAt;
    }

    public static function order(
        UserId $userId,
        Firstname $firstname,
        Lastname $lastname,
        OrderValue $value,
        Status $status,
        ?Payment $payment,
        Subscription $subscription
    ): self {
        return new self(
            OrderId::newOne(),
            $userId,
            $firstname,
            $lastname,
            $value,
            $status,
            $subscription,
            $payment,
            Clock::system()->currentDateTime()
        );
    }

    public function markSucceded(): Result
    {
        $this->status = Status::succeded();
        return Result::success();
    }

    public function markFailed(): Result
    {
        if ($this->status == Status::succeded()) {
            return Result::failure('Succeded order cannot be marked as failed');
        }
        if ($this->status == Status::failed()) {
            return Result::failure('Failed order cannot be marked as failed');
        }
        $this->status = Status::failed();
        return Result::success();
    }

    /**
     * @return array|mixed
     */
    public function jsonSerialize()
    {
        return [
            'id' => (string) $this->id,
            'status' => $this->status->getValue(),
            'subscription' => $this->subscription->jsonSerialize()
        ];
    }

    /**
     * @return Subscription
     */
    public function getSubscription(): Subscription
    {
        return $this->subscription;
    }

    /**
     * @return OrderValue
     */
    public function getOrderValue(): OrderValue
    {
        return $this->orderValue;
    }

    /**
     * @return OrderId
     */
    public function getId(): OrderId
    {
        return $this->id;
    }

    /**
     * @return UserId
     */
    public function getUserId(): UserId
    {
        return $this->userId;
    }

    public static function createRecurredOrder(self $oldOrder): self
    {
        return new self(
            OrderId::newOne(),
            $oldOrder->userId,
            $oldOrder->firstname,
            $oldOrder->lastname,
            $oldOrder->orderValue,
            Status::processing(),
            $oldOrder->subscription,
            $oldOrder->payment,
            Clock::system()->currentDateTime()
        );
    }

    public function addPayment(Payment $payment): void
    {
        $this->payment = $payment;
    }

    /**
     * @return Payment
     */
    public function getPayment(): Payment
    {
        return $this->payment;
    }
}