#!/bin/sh
if [ -f .env ]
then
  export $(cat .env | sed 's/#.*//g' | xargs)
fi
sed -i "s/MSSQL_PASSWORD_IT_WILL_BE_REPLACED/$MS_SQL_PASSWORD/g" src/Fixit.WebApi/appsettings.json
docker-compose up -d
echo "Waiting until mysql boots up"
sleep 10
docker-compose exec -u www-data php-fpm composer install
docker-compose exec -u www-data php-fpm bin/console doctrine:migrations:migrate --no-interaction
