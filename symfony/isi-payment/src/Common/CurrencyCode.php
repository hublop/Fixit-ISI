<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Common;

use Doctrine\ORM\Mapping as ORM;
use MyCLabs\Enum\Enum;

/**
 * Class CurrencyCode
 * @package App\Domain\Payment
 * @method static self pln
 * @method static self eur
 * @method static self usd
 * @ORM\Embeddable
 */
class CurrencyCode extends Enum
{
    private const pln = 'PLN';
    private const eur = 'EUR';
    private const usd = 'USD';

    /**
     * @var string
     * @ORM\Column(type="string", name="currency_code")
     */
    protected $value;
}