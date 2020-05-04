<?php
/**
 * @category    symfony
 * @date        02/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Common;

abstract class Clock
{
    abstract public function currentDateTime(): \DateTimeImmutable;

    public static function system(): self
    {
        return new SystemClock();
    }

    public static function fixed(\DateTimeImmutable $dateTime): self
    {
        return new FixedClock($dateTime);
    }
}