<?php

namespace App\Domain\Order;

use MyCLabs\Enum\Enum;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Status
 * @package App\Domain\Order
 * @method static self processing()
 * @method static self failed()
 * @method static self succeded()
 * @ORM\Embeddable
 */
class Status extends Enum
{
    private const processing = 'PROCESSING';
    private const failed = 'FAILED';
    private const succeded = 'SUCCEDED';

    /**
     * @var string
     * @ORM\Column(type="string", name="status")
     */
    protected $value;
}