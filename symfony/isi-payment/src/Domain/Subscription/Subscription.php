<?php

namespace App\Domain\Subscription;

use App\Common\Clock;
use App\Common\Email;
use App\Common\Firstname;
use App\Common\Lastname;
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
     * @var Name
     * @ORM\Embedded(class="App\Domain\Subscription\Name", columnPrefix=false)
     */
    private Name $name;
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
     * @var Firstname
     * @ORM\Embedded(class="App\Common\Firstname", columnPrefix=false)
     */
    private Firstname $firstname;

    /**
     * @var Lastname
     * @ORM\Embedded(class="App\Common\Lastname", columnPrefix=false)
     */
    private Lastname $lastname;

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

    /**
     * @var UserId
     * @ORM\Embedded(class="App\Domain\Subscription\UserId", columnPrefix=false)
     */
    private UserId $userId;
    /**
     * @var Cost
     * @ORM\Embedded(class="App\Domain\Subscription\Cost", columnPrefix=false)
     */
    private Cost $cost;

    public function __construct(
        SubscriptionId $id,
        Name $name,
        Status $status,
        Cost $cost,
        UserId $userId,
        Email $email,
        Firstname $firstname,
        Lastname $lastname,
        \DateTimeImmutable $dateTime,
        \DateTimeImmutable $nextPaymentDate
    ) {
        $this->userId          = $userId;
        $this->name            = $name;
        $this->id              = $id;
        $this->cost            = $cost;
        $this->status          = $status;
        $this->email           = $email;
        $this->firstname       = $firstname;
        $this->lastname        = $lastname;
        $this->dateTime        = $dateTime;
        $this->nextPaymentDate = $nextPaymentDate;
    }

    public static function newSubscription(
        Name $name,
        UserId $userId,
        Cost $cost,
        Email $email,
        Firstname $firstname,
        Lastname $lastname
    ): self {
        return new self(
            SubscriptionId::newOne(),
            $name,
            Status::inactive(),
            $cost,
            $userId,
            $email,
            $firstname,
            $lastname,
            Clock::system()->currentDateTime(),
            Clock::system()->currentDateTime()->add(\DateInterval::createFromDateString("1 month"))
        );
    }

    public function activate(): Result
    {
        $this->status          = Status::active();
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

    public function cancel(): Result
    {
        if ($this->status != Status::active()) {
            return Result::failure("Only active subscriptions can be cancelled");
        }
        $this->status = Status::cancelled();
        return Result::success();
    }

    public function isActive(): bool
    {
        return $this->status->equals(Status::active());
    }

    public function isDisabled(): bool
    {
        return $this->status->equals(Status::disabled());
    }


    public function isCancelled(): bool
    {
        return $this->status->equals(Status::cancelled());
    }

    public function id(): SubscriptionId
    {
        return $this->id;
    }

    public function jsonSerialize()
    {
        return [
            'id'     => (string)$this->id,
            'name'   => (string)$this->name,
            'userId' => (string)$this->userId,
            'status' => $this->status->getValue(),
            'email'  => (string)$this->email
        ];
    }

    /**
     * @return Email
     */
    public function getEmail(): Email
    {
        return $this->email;
    }

    /**
     * @return \DateTimeImmutable
     */
    public function getDateTime(): \DateTimeImmutable
    {
        return $this->dateTime;
    }

    /**
     * @return \DateTimeImmutable
     */
    public function getNextPaymentDate(): \DateTimeImmutable
    {
        return $this->nextPaymentDate;
    }

    /**
     * @return UserId
     */
    public function getUserId(): UserId
    {
        return $this->userId;
    }

    /**
     * @return Cost
     */
    public function getCost(): Cost
    {
        return $this->cost;
    }

    /**
     * @return Firstname
     */
    public function getFirstname(): Firstname
    {
        return $this->firstname;
    }

    /**
     * @return Lastname
     */
    public function getLastname(): Lastname
    {
        return $this->lastname;
    }
}