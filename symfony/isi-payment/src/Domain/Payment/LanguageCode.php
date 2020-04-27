<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use MyCLabs\Enum\Enum;

/**
 * Class LanguageCode
 * @package App\Domain\Payment
 * @method static self pl
 * @method static self en
 * @method static self de
 */
class LanguageCode extends Enum
{
    private const pl = 'pl';
    private const en = 'en';
    private const de= 'de';
}