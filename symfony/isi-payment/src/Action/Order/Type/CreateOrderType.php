<?php

namespace App\Action\Order\Type;

use App\Common\CurrencyCode;
use App\Common\Email;
use App\Common\ExternalUUID;
use App\Common\Type\AbstractType;
use App\Domain\Order\Firstname;
use App\Domain\Order\Lastname;
use App\Domain\Order\OrderValue;
use App\Domain\Order\UserId;
use Symfony\Component\Validator\Constraints as Assert;

/**
 * Class CreateOrderType
 * @package App\Action\Order\Type
 */
class CreateOrderType extends AbstractType
{
    /**
     * @var UserId|null
     * @Assert\NotNull(message="userId must not be null ")
     */
    public ?UserId $userId = null;
    /**
     * @var Email|null
     * @Assert\NotNull(message="userEmail must not be null")
     */
    public ?Email $userEmail = null;
    /**
     * @var Firstname|null
     * @Assert\NotNull(message="firstname must not be null")
     */
    public ?Firstname $firstname = null;
    /**
     * @var Lastname|null
     * @Assert\NotNull(message="lastname must not be null")
     */
    public ?Lastname $lastname = null;
    /**
     * @var OrderValue|null
     * @Assert\NotNull(message="totalAmount must not be null")
     */
    public ?OrderValue $totalAmount = null;

    /**
     * @param string $firstname
     * @return CreateOrderType
     */
    public function setFirstname(string $firstname): CreateOrderType
    {
        $this->firstname = new Firstname($firstname);
        return $this;
    }

    /**
     * @param string $lastname
     * @return CreateOrderType
     */
    public function setLastname(string $lastname): CreateOrderType
    {
        $this->lastname = new Lastname($lastname);
        return $this;
    }

    /**
     * @param int $totalAmount
     * @return CreateOrderType
     */
    public function setTotalAmount(int $totalAmount): CreateOrderType
    {
        if (!is_int($totalAmount)) {
            throw new \InvalidArgumentException("Order value must be an integer");
        }
        $this->totalAmount = new OrderValue($totalAmount, CurrencyCode::pln());
        return $this;
    }

    public function setUserEmail(string $email): self
    {
        $this->userEmail = new Email($email);
        return $this;
    }

    public function setUserId(string $id): self
    {
        $this->userId = new UserId(new ExternalUUID($id));
        return $this;
    }
}
