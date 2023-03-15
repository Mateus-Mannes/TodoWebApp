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
