FROM php:7.4.4-fpm-alpine3.11
RUN apk --no-cache add \
        autoconf \
        automake \
        gcc \
        g++ \
        make \
        libpng-dev \
        zlib-dev \
        icu-dev \
        openssl-dev \
        libressl-dev \
        libmemcached-dev \
        rabbitmq-c-dev \
    && pecl install amqp \
    && docker-php-ext-configure gd \
    && printf "\n" | pecl install memcached \
    && printf "\n" | pecl install apcu \
    && echo "extension=memcached.so" > /usr/local/etc/php/conf.d/memcached.ini \
    && echo "extension=apcu.so" > /usr/local/etc/php/conf.d/apcu.ini \
    && docker-php-ext-install \
        bcmath \
        sockets \
        pdo_mysql \
        intl \
        gd \
        opcache \
        mysqli \
    && docker-php-ext-enable amqp


RUN apk add shadow
RUN usermod -d /var/www -u 1000 www-data
RUN groupmod -g 1000 www-data
RUN chown -R 1000:1000 /var/www
WORKDIR /var/www/isi-payment

RUN (curl -sS https://getcomposer.org/installer | php) || exit 1 && mv composer.phar /usr/sbin/composer && chmod 0755 /usr/sbin/composer

USER www-data
RUN mkdir /var/www/.composer
RUN composer global require hirak/prestissimo:0.3.9 \
    --prefer-dist \
    --update-no-dev \
    --no-suggest \
    --no-interaction

WORKDIR /var/www/isi-payment
CMD ["php-fpm"]
