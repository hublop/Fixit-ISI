<?php

namespace App\EventListener;

use App\Common\Result;
use App\Exception\ApiErrorException;
use App\Responder\JsonResponder;
use Symfony\Component\EventDispatcher\EventSubscriberInterface;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpKernel\Event\ExceptionEvent;
use Symfony\Component\HttpKernel\KernelEvents;

class ApiExceptionSubscriber  implements EventSubscriberInterface
{
    /**
     * @var JsonResponder
     */
    private JsonResponder $responder;

    public function __construct(JsonResponder $responder)
    {
        $this->responder = $responder;
    }

    /**
     * @return array
     */
    public static function getSubscribedEvents()
    {
        return [
            KernelEvents::EXCEPTION => 'onKernelException'
        ];
    }

    /**
     * @param ExceptionEvent $event
     * @return void
     */
    public function onKernelException(ExceptionEvent $event)
    {
        $exception = $event->getThrowable();
        if ($exception instanceof \InvalidArgumentException) {
            $event->setResponse($this->responder->respond(
                Result::failure($exception->getMessage(), Response::HTTP_BAD_REQUEST))
            );
        }
        if (!$exception instanceof ApiErrorException) {
            return;
        }
        $event->setResponse($this->responder->respond(Result::failure($exception->getMessage(), $exception->getCode())));
    }
}
