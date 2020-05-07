<?php

namespace App\Domain\Subscription;

use App\Common\Clock;
use App\Common\Email;
use App\Common\Result;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Subscription
 * @package App\Domain\Subscription
 * @ORM\Entity(repositoryClass="App\Infrastructure\Doctrine\SubscriptionRepository")
 */
class Subscription implements \JsonSerializable
{
    /**
     * @var SubscriptionId
     * @ORM\Embedded(class="App\Domain\Subscription\SubscriptionId", columnPrefix=false )
     */
    private SubscriptionId $id;
    /**
     * @var Status
     * @ORM\Embedded(class="App\Domain\Subscription\Status", columnPrefix=false)
     */
    private Status $status;

    /**
     * @var Email
     * @ORM\Embedded(class="App\Common\Email", columnPrefix=false)
     */
    private Email $email;

    /**
     * @var \DateTimeImmutable
     * @ORM\Column(name="created_at", type="datetime_immutable")
     */
    private \DateTimeImmutable $dateTime;

    /**
     * @var \DateTimeImmutable
     * @ORM\Column(name="next_payment_date", type="datetime_immutable")
     */
    private \DateTimeImmutable $nextPaymentDate;

    public function __construct(
        SubscriptionId $id,
        Status $status,
        Email $email,
        \DateTimeImmutable $dateTime,
        \DateTimeImmutable $nextPaymentDate
    ) {
        $this->id = $id;
        $this->status = $status;
        $this->email = $email;
        $this->dateTime = $dateTime;
        $this->nextPaymentDate = $nextPaymentDate;
    }

    public static function subscription(Status $status, Email $email): self
    {
        return new self(
            SubscriptionId::newOne(),
            $status,
            $email,
            Clock::system()->currentDateTime(),
            Clock::system()->currentDateTime()->add(\DateInterval::createFromDateString("1 month"))
        );
    }

    public function activate(): Result
    {
        $this->status = Status::active();
        $this->nextPaymentDate = Clock::system()->currentDateTime()->add(\DateInterval::createFromDateString("1 month"));
        return Result::success();
    }

    public function disable(): Result
    {
        if ($this->status === Status::disabled()) {
            return Result::failure("This subscription is already disabled.", 400);
        }
        $this->status = Status::disabled();

        return Result::success();
    }

    public function markAsPastDue(): Result
    {
        $this->status = Status::pastDue();

        return Result::success();
    }

    public function isActive(): bool
    {
        return $this->status->equals(Status::activated());
    }

    public function isDisabled(): bool
    {
        return $this->status->equals(Status::disabled());
    }

    public function isPastDue(): bool
    {
        return $this->status->equals(Status::pastDue());
    }

    public function id(): SubscriptionId
    {
        return $this->id;
    }

    public function jsonSerialize()
    {
        return [
            'id' => (string) $this->id,
            'status' => $this->status->getValue(),
            'email' => (string) $this->email
        ];
    }

    /**
     * @return Email
     */
    public function getEmail(): Email
    {
        return $this->email;
    }
}