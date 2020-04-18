<?php

namespace App\Common\Type;

use Symfony\Component\HttpFoundation\Request;

abstract class AbstractType
{
    /**
     * AbstractQuery constructor.
     * @param Request|null $request
     */
    public function __construct(Request $request = null)
    {
        if (null !== $request) {
            $params = array_merge(
                $request->attributes->all(),
                $request->query->all(),
                $request->request->all(),
                json_decode($request->getContent(), true) ?? []
            );
            $this->setParams($params);
        }
    }
    /**
     * @param array $params
     * @return void
     */
    public function setParams(array $params): void
    {
        foreach ($params as $key => $value) {
            $setter = 'set' . ucfirst($key);
            if (is_callable([$this, $setter])) {
                $this->{$setter}($this->prepareValue($value));
            }
        }
    }
    private function prepareValue($value)
    {
        if (is_string($value)) {
            return htmlspecialchars($value);
        }
        if (is_array($value)) {
            $result = [];
            foreach ($value as $val) {
                $result[] = is_string($val) ? htmlspecialchars($val) : $val;
            }
            return $result;
        }
        return $value;
    }
}