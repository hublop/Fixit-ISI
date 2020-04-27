<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use MyCLabs\Enum\Enum;
use Doctrine\ORM\Mapping as ORM;

/**
 * Class Status
 * @package App\Domain\Payment
 * @method static self success()
 * @method static self warning()
 * @method static self error()
 * @ORM\Embeddable
 */
class Status extends Enum
{
    /**
     * @var string
     * @ORM\Column(type="string", name="status")
     */
    protected $value;

    public const success = 'SUCCESS';
    public const warning = 'WARNING';
    public const error = 'ERROR_INTERNAL';
}