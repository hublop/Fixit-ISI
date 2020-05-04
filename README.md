# <a name="fixit"></a>Fixit-ISI

This is a sample integration project Fix.IT to present different intergation solutions.

**Table of Contents**
- [Fixit-ISI](#fixit)
- [Description](#description)
- [Requirements](#requirements)
- [Installation](#installation)
- [Uninstallation](#remove)

## <a name="description"></a> Description
This system consits of 7 Docker containers:
 - dotnet - Main .NET Core based application
 - frontend - Angluar based application
 - mssql - Microsoft SQL Database
 - db - MySQL database for payment processing microservice
 - php_fpm - Symfony PHP based application for payment processing
 - nginx - Nginx server for Symfony payment microservice 
 - rabbitMQ - queue system for messages processing

## <a name="requirements"></a> Requirements
In order to setup this system you must have installed: 
- Docker (version 18 or later)
- docker-compose (1.20 or later)
## <a name="installation"></a> Installation
To setup whole environment:
1. Run:

        git clone https://github.com/hublop/Fixit-ISI.git 
        cd Fixit-ISI
2. Change MS_SQL_PASSWORD password located in .env file
3. Run:

        sh ./setup.sh
        
## <a name="remove"></a> Uninstallation
To tear down whole environment and databases:
1. Run

        docker-compose down -v