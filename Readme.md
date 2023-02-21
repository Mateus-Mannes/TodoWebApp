# TodoWebApp
This is an API built to work as a Todo App backend, built on DOTNET 6 

## Running
using DOTNET 6 CLI, run:
``` cmd
dotnet run
````
in the project directory

## Structure 

the application code is in the `/src` directory, where you will find the main project. The `/test` directory contains the main testing project

### /src main layers

- Controllers: All endpoints
- Domain: Domain entities
- Extensions: Extension methods classes
- Migrations: EF Core migrations
- Data: DbContext and DataMappings
- Repositories: Repository pattern implementation
- Services: Injectable services classes
- ViewModels: ViewModels classes
 
### Observations about the /src code

 - As the objective of this project is just practicing C# and DOTNET, the database structure is a simple SQLITE, and the authorization is also very simple and not very secure

 ### /test architecture

 The tests are simple, all in one file `TodoAppTests`. It inherits from `TodoAppTestBase`, that implements an in-memory sqlite for the test data and have a ServiceCollection that provides access to dbcontext, services, controllers, etc ...  

