# FlightOrder ![run-tests](https://github.com/adamijak/flight-order/actions/workflows/run-tests.yml/badge.svg)

## Installation

You must have docker installed.

To run aplication download source code,
navigate to root folder of application and run following command:

`docker compose -f docker-compose.yml -f docker-compose.prod.yml up -d --build`

Then you can open your browser on:

`http://localhost`

To run test navigate to root folder of application and run:

`docker compose -f docker-compose.yml -f docker-compose.test.yml up --exit-code-from webapptest --build`
