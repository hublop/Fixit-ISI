<?php
/**
 * @category    Fixit-ISI
 * @date        10/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */
namespace App\Action\Subscription\Type;

use App\Common\CurrencyCode;
use App\Common\Email;
use App\Common\ExternalUUID;
use App\Common\Firstname;
use App\Common\Lastname;
use App\Common\Type\AbstractType;
use App\Domain\Subscription\Cost;
use App\Domain\Subscription\Name;
use App\Domain\Subscription\UserId;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\Validator\Constraints as Assert;

class CreateSubscriptionType extends AbstractType
{
    /**
     * @var Name|null
     * @Assert\NotNull(message="name must not be null ")
     */
    public ?Name $name = null;

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
     * @var \App\Domain\Order\\App\Common\Lastname|null
     * @Assert\NotNull(message="lastname must not be null")
     */
    public ?Lastname $lastname = null;
    /**
     * @var Cost|null
     * @Assert\NotNull(message="cost must not be null")
     */
    public ?Cost $cost = null;

    /**
     * @param string $firstname
     * @return CreateSubscriptionType
     */
    public function setFirstname(string $firstname): CreateSubscriptionType
    {
        $this->firstname = new Firstname($firstname);
        return $this;
    }

    /**
     * @param string $lastname
     * @return CreateSubscriptionType
     */
    public function setLastname(string $lastname): CreateSubscriptionType
    {
        $this->lastname = new Lastname($lastname);
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

    /**
     * @param string $name
     * @return CreateSubscriptionType
     */
    public function setName(string $name): self
    {
        $this->name = new Name($name);
        return $this;
    }

    /**
     * @param string $cost
     */
    public function setCost($cost): self
    {
        if (!is_numeric($cost)) {
            throw new \InvalidArgumentException("Order value must be an integer");
        }
        $this->cost = new Cost(intval($cost), CurrencyCode::pln());
        return $this;
    }
    public function __construct(Request $request = null)
    {
        parent::__construct($request);
        $this->name = new Name("Fit.IT Premium account");

    }

}