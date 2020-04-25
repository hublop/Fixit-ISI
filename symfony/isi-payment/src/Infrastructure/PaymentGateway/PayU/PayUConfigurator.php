<?php
/**
 * @category    symfony
 * @date        25/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Infrastructure\PaymentGateway\PayU;

use OpenPayU_Configuration;

class PayUConfigurator
{

    private array $config;

    public function __construct(array $payuConfig)
    {
        $this->config = $payuConfig;
    }

    public function configurePayment()
    {
        OpenPayU_Configuration::setEnvironment($this->config['env']);
        OpenPayU_Configuration::setMerchantPosId($this->config['posId']);
        OpenPayU_Configuration::setSignatureKey($this->config['privateKey']);
        OpenPayU_Configuration::setOauthClientId($this->config['oauthClientId']);
        OpenPayU_Configuration::setOauthClientSecret($this->config['oauthClientSecret']);
    }
}