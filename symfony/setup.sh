#!/bin/sh
docker-compose up -d
echo "Waiting until mysql boots up"
sleep 20
docker-compose exec -u www-data php-fpm composer install
docker-compose exec -u www-data php-fpm bin/console doctrine:migrations:migrate --no-interaction
