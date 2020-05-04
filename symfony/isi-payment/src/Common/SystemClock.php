<?php
/**
 * @category    symfony
 * @date        02/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Common;

final class SystemClock extends Clock
{
    public function currentDateTime(): \DateTimeImmutable
    {
        return new \DateTimeImmutable("now");
    }
}