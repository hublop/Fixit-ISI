<?php

namespace App\Infrastructure\Doctrine;

use App\Common\UUID;
use App\Domain\Subscription\Name;
use App\Domain\Subscription\Status;
use App\Domain\Subscription\Subscription;
use App\Domain\Subscription\UserId;
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

    public function findByDateStatuses(\DateTimeImmutable $dateTimeImmutable, array $statuses)
    {
        $qb = $this->createQueryBuilder("e");
        $qb
            ->where('e.nextPaymentDate < :to AND e.status.value IN(:statuses)')
            ->setParameter('to', $dateTimeImmutable)
            ->setParameter('statuses', $statuses);
        return $qb->getQuery()->getResult();
    }

    public function getActiveUserSubscriptions(Name $name, UserId $userId)
    {
        $qb = $this->createQueryBuilder("e");
        $qb
            ->where(
                'e.name.value = :name AND e.userId.id.value = :userId AND (e.status.value = :statusActive OR e.status.value = :statusCancelled)'
            )
            ->setParameter('name', (string) $name)
            ->setParameter('userId', (string) $userId)
            ->setParameter('statusActive', Status::active()->getValue())
            ->setParameter('statusCancelled', Status::cancelled()->getValue());
        return $qb->getQuery()->getResult();

    }
}