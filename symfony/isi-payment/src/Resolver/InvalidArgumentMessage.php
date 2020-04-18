<?php

namespace App\Resolver;

use Symfony\Component\Validator\ConstraintViolationInterface;
use Symfony\Component\Validator\ConstraintViolationListInterface;

class InvalidArgumentMessage
{
    /**
     * @param ConstraintViolationListInterface $errors
     * @return array
     */
    public function getErrors($errors)
    {
        $response = [];

        /** @var ConstraintViolationInterface $error */
        foreach ($errors as $error) {
            $response[] = $error->getMessage();
        }

        return $response;
    }
}