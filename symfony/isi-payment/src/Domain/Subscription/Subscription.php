<?php

namespace App\Domain\Subscription;

use App\Common\Email;
use App\Common\Result;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Subscription
 * @package App\Domain\Subscription
 * @ORM\Entity(repositoryClass="App\Infrastructure\SubscriptionRepository")
 */
final class Subscription implements \JsonSerializable
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

    public function __construct(
        SubscriptionId $id,
        Status $status,
        Email $email
    ) {
        $this->id = $id;
        $this->status = $status;
        $this->email = $email;
    }

    public static function subscription(Status $status, Email $email): self
    {
        return new self(
            SubscriptionId::newOne(),
            $status,
            $email
        );
    }

    public function activate(): Result
    {
        $this->status = Status::activated();
        return Result::success();
    }

    public function disable(): Result
    {
        if ($this->status == Status::disabled()) {
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
            'email' => (string)$this->email
        ];
    }
}