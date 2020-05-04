<?php

namespace App\Common\Result;

use App\Common\Result;

class Failure extends Result
{

    protected string $reason;

    protected int $code;

    public function __construct(string $reason, int $code)
    {
        $this->reason = $reason;
        $this->code = $code;
    }

    /**
     * @return array
     */
    public function jsonSerialize()
    {
        return [
            'success' => false,
            'reason' => $this->reason
        ];
    }

    /**
     * @return int
     */
    public function getCode()
    {
        return $this->code;
    }
    public function __toString()
    {
        return "Failure! " . $this->reason;
    }
}