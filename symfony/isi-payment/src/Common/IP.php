<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Common;

class IP
{
    private string $ip;

    public function __construct(string $ip)
    {
        $value = filter_var($ip, FILTER_VALIDATE_IP);
        if (!$value) {
            throw new \InvalidArgumentException('Invalid IP format');
        }
        $this->ip = $value;
    }

    public function __toString()
    {
        return $this->ip;
    }

    public static function localIP(): self
    {
        return new self('127.0.0.1');
    }
}