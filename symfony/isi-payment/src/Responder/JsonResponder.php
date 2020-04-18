<?php

namespace App\Responder;

use App\Common\Result\Failure;
use Symfony\Component\HttpFoundation\JsonResponse;

/**
 * Class JsonResponder
 * @package App\Responder
 */
class JsonResponder
{
    /**
     * @param null  $data
     * @param int   $status
     * @param array $headers
     * @param bool  $json
     * @return JsonResponse
     */
    public function respond($data = null, int $status = 200, array $headers = [], bool $json = false): JsonResponse
    {
        if ($data instanceof Failure) {
            $status = $data->getCode();
        }
        return new JsonResponse($data, $status, $headers, $json);
    }
}