<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Action\Order\Type;

use App\Common\Type\AbstractType;
use App\Common\UUID;

class GetOrderType extends AbstractType
{
    public ?UUID $uuid;
    /**
     * @param string $uuid
     * @return GetOrderType
     */
    public function setUuid(string $uuid): GetOrderType
    {
        $this->uuid = new UUID($uuid);
        return $this;
    }
}