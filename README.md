# ToysNGames

This solution does CRUD operations on a database.
It is divided in 2 parts: Server and Client.

## Server

Contains 3 projects:

* ToysNGames.api: REST API using .NET Core 3.1
* ToysNGames.data: Data layer with Entity Framework. The database is an in-memory database.
* ToysNGames.tests: MSTest unit tests using Moq

## Client

Contains 1 project:

* ToysNGames.app: Angular 9 app with SASS and Material UI

## Run instructions

1. Localhost port is set as 50114 on both ToysNGames.api and ToysNGames.app projects. If you want to change it, please update both projects.
In ToysNGames.api, go to Properties -> Debugging -> Application URL. Modify URL with the new port.
In ToysNGames.app, go to folder src -> app -> shared -> product-service.ts. Change PRODUCT_API_SERVICE const with the new port.
2. Run ToysNGames.api
3. Run ToysNGames.app