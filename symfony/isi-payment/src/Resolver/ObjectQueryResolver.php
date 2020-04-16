<?php

namespace App\Resolver;


use App\Common\Type\AbstractType;
use App\Exception\ApiErrorException;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpKernel\Controller\ArgumentValueResolverInterface;
use Symfony\Component\HttpKernel\ControllerMetadata\ArgumentMetadata;
use Symfony\Component\Validator\Validator\ValidatorInterface;

class ObjectQueryResolver implements ArgumentValueResolverInterface
{
    /** @var ValidatorInterface $validator */
    private $validator;

    /**
     * QueryObjectResolver constructor.
     * @param ValidatorInterface $validator
     */
    public function __construct(ValidatorInterface $validator)
    {
        $this->validator = $validator;
    }

    /**
     * @param Request $request
     * @param ArgumentMetadata $argument
     * @return bool
     * @throws \ReflectionException
     */
    public function supports(Request $request, ArgumentMetadata $argument)
    {
        if (!$argument->getType() || !class_exists($argument->getType())) {
            return false;
        }

        $reflection = new \ReflectionClass($argument->getType());

        return $reflection->isSubclassOf(AbstractType::class);
    }

    /**
     * @param Request $request
     * @param ArgumentMetadata $argument
     * @return \Generator
     */
    public function resolve(Request $request, ArgumentMetadata $argument)
    {
        $class = $argument->getType();

        $dto = new $class($request);
        $errors = $this->validator->validate($dto);

        if (count($errors) > 0) {
            $response = (new InvalidArgumentMessage())->getErrors($errors);
            throw new ApiErrorException(
                implode(" " . PHP_EOL . " ", $response),
                Response::HTTP_BAD_REQUEST
            );
        }
        yield $dto;
    }
}
