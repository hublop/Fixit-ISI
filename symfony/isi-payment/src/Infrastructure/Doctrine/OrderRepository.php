<?php

namespace App\Infrastructure\Doctrine;

use App\Common\UUID;
use App\Domain\Order\Order;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class OrderRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Order::class);
    }

    public function findByUUID(UUID $uuid)
    {
        return $this->getEntityManager()
            ->find(Order::class, (string) $uuid);
    }
}