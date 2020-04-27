<?php
/**
 * @category    symfony
 * @date        24/04/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Domain\Payment;

use App\Common\CurrencyCode;
use App\Common\Email;
use App\Domain\Order\Order;

class PaymentWidget implements \JsonSerializable
{
    private string $payButton = "#pay-button";
    private MerchantPosId $merchantPosId;
    private ShopName $shopName;
    private TotalAmount $totalAmount;
    private CurrencyCode $currencyCode;
    private LanguageCode $customerLanguage;
    private bool $storeCard;
    private bool $recurringPayment;
    private Email $customerEmail;
    private Signature $signature;

    public static function fromOrder(
        Order $order,
        MerchantPosId $posId,
        ShopName $shopName,
        LanguageCode $customerLang,
        bool $storeCard,
        bool $isRecurring,
        string $privateKey
    ): self {
        $object                   = new self;
        $object->totalAmount      = new TotalAmount($order->getOrderValue()->getValue());
        $object->customerEmail    = $order->getSubscription()->getEmail();
        $object->currencyCode     = $order->getOrderValue()->getCurrencyCode();
        $object->customerLanguage = $customerLang;
        $object->storeCard        = $storeCard;
        $object->recurringPayment = $isRecurring;
        $object->merchantPosId    = $posId;
        $object->shopName         = $shopName;
        $object->signature        = Signature::getForPaymentWidget($object, $privateKey);
        return $object;
    }

    public function jsonSerialize(): array
    {
        return [
            'pay-button'        => $this->payButton,
            'merchant-pos-id'   => (string)$this->merchantPosId,
            'shop-name'         => (string)$this->shopName,
            'total-amount'      => (string)$this->totalAmount,
            'currency-code'     => (string)$this->currencyCode,
            'customer-language' => (string)$this->customerLanguage,
            'store-card'        => $this->storeCard,
            'recurring-payment' => $this->recurringPayment,
            'customer-email'    => (string)$this->customerEmail,
            'sig'               => (string)$this->signature
        ];
    }

    /**
     * @return MerchantPosId
     */
    public function getMerchantPosId(): MerchantPosId
    {
        return $this->merchantPosId;
    }

    /**
     * @return ShopName
     */
    public function getShopName(): ShopName
    {
        return $this->shopName;
    }

    /**
     * @return TotalAmount
     */
    public function getTotalAmount(): TotalAmount
    {
        return $this->totalAmount;
    }

    /**
     * @return CurrencyCode
     */
    public function getCurrencyCode(): CurrencyCode
    {
        return $this->currencyCode;
    }

    /**
     * @return LanguageCode
     */
    public function getCustomerLanguage(): LanguageCode
    {
        return $this->customerLanguage;
    }

    /**
     * @return Email
     */
    public function getCustomerEmail(): Email
    {
        return $this->customerEmail;
    }
}