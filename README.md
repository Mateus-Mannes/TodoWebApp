# TodoApi

This is an API built to work as a Todo App backend, built on DOTNET 8

## Running
using DOTNET 8 CLI, run:
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

# TodoWebApp

This is a simple TodoApp front end built on Angular

## Running

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.2.6.

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`.

the  application is using the API at https://github.com/Mateus-Mannes/TodoWebApp for the back-end. You can run it locally also.

## Project Structure

``` mermaid
flowchart TB
  subgraph Login Page
    LoginComponent
  end

  subgraph Create User Page
    CreateUserComponent
  end

  subgraph Application Page
    
    ApplicationComponent
    
    subgraph GroupsModule
      GroupsComponent[GroupsComponent \n - Groups CRUD]
    end

    ApplicationComponent -- Fills groups --> GroupsModule

    subgraph GridModule
      GridComponent[GridComponent \n - Todos CRUD]
      -- 1 - * -->
      TodoComponent

      TodoComponent -- pop up -->EditTodoComponent
    end

    GroupsModule -- Selected group \nfills the grid --> GridModule

    subgraph InputModule
      InputComponent[InputComponent]
    end

    InputModule -- emits todo creation --> GridModule

  end
```
