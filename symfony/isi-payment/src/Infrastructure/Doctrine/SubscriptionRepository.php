<?php

namespace App\Infrastructure\Doctrine;

use App\Common\UUID;
use App\Domain\Subscription\Subscription;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class SubscriptionRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Subscription::class);
    }

    public function findByUUID(UUID $uuid)
    {
        return $this->getEntityManager()
            ->find(Subscription::class, (string) $uuid);
    }

    public function findByDateStatus(\DateTimeImmutable $dateTimeImmutable, string $status)
    {
        $qb = $this->createQueryBuilder("e");
        $qb
            ->where('e.nextPaymentDate < :to AND e.status.value = :status')
            ->setParameter('to', $dateTimeImmutable)
            ->setParameter('status', $status);
        return $qb->getQuery()->getResult();
    }
}