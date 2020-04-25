<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Common;


class Url
{
    private string $value;

    public function __construct(string $url)
    {
        if (!preg_match('%^((https?://)|(www\.))([a-z0-9-].?)+(:[0-9]+)?(/.*)?$%i', $url)) {
            throw new \InvalidArgumentException('This url is malformatted.');
        }
        $this->value = $url;
    }

    public function __toString()
    {
        return $this->value;
    }
}