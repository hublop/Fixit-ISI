#!/bin/sh
if [ -f .env ]
then
  export $(cat .env | sed 's/#.*//g' | xargs)
fi
sed -i "s/MSSQL_PASSWORD_IT_WILL_BE_REPLACED/$MSSQL_PASSWORD/g" src/Fixit.WebApi/appsettings.json
cp symfony/isi-payment/.env symfony/isi-payment/.env.local
sed -i "s/RABBIT_USER/$RABBIT_USER/g" symfony/isi-payment/.env.local
sed -i "s/RABBIT_PASS/$RABBIT_PASS/g" symfony/isi-payment/.env.local
docker-compose down -v
docker-compose build dotnet
docekr-compose build frontend
docker-compose up -d
echo "Waiting until mysql boots up"
sleep 15
docker-compose exec -u www-data php-fpm composer install
docker-compose exec -u www-data php-fpm bin/console doctrine:migrations:migrate --no-interaction
