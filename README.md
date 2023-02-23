# TodoApp

This is a simple TodoApp front end built on Angular

## Running

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.2.6.

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`.

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
    
    subgraph ApplicationModel
      ApplicationComponent
    end
    
    subgraph GroupsModel
      GroupsComponent[GroupsComponent \n - Groups CRUD]
    end

    ApplicationModel -- Fills groups --> GroupsModel

    subgraph GridModel
      GridComponent[GridComponent \n - Todos CRUD]
      -- 1 - * -->
      TodoComponent
    end

    GroupsModel -- Fills the grid --> GridModel

    subgraph InputModel
      InputComponent
    end

    InputModel -- Adds todos --> GridModel

  end
```
