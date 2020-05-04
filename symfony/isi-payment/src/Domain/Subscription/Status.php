<?php

namespace App\Domain\Subscription;

use MyCLabs\Enum\Enum;
use Doctrine\ORM\Mapping as ORM;

/**
 * @method static self active()
 * @method static self pastDue()
 * @method static self disabled()
 * @method static self inactive()
 * @ORM\Embeddable
 */
class Status extends Enum
{
    private const active = 'active';
    private const pastDue = 'pastDue';
    private const disabled = 'disabled';
    private const inactive = 'inactive';
    /**
     * @var string
     * @ORM\Column(type="string", name="status")
     */
    protected $value;
}