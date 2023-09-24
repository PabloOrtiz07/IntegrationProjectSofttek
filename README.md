# Softtek Integration Project (C#)

This project is developed with .NET Core 6.

## Softtek Academy .NET Project

This project is an individual academic assignment for Softtek, which involves the use of .NET Core 6 and SQL Server 2019 for database management.

## Description

The primary goal of this project is to create a RESTful API for the initial deliverable. For the second delivery, we plan to utilize Blazor for the frontend and develop any required controllers or endpoints.

## Architecture Details

### Controller Layer
This layer is used to communicate with the API, where it serves as the endpoint for our project.

### DataAccess Layer
In this layer, we set up our DbContext and create seeders for our entities.

### Entities Layer
This layer is used to declare all entities for our database.

### Repositories Layer
This layer is responsible for declaring our classes to interact with our database.

- Helper: It is used to declare useful logic for our project, such as methods for encrypting/decrypting passwords.
- Interfaces: In this section, we declare which methods will be used.
- Mapper: In this file, we declare mapping classes to link entity-dto or dto-entity.

## Git Statements

The rule we will follow is to create branches from Dev. The naming convention is as follows: feature/xxx (where xxx is the branch name).

## Assignment Link

[Link to Assignment Document](https://drive.google.com/file/d/1yIRq0M9FdApUU2c8_OUoryahJM9B9cO8/view)

## Author

Author: Ortiz Pablo

## Acknowledgments

I acknowledge this project to the Softtek team for allowing me to participate in this .NET Academy. I extend my gratitude to Marcio Abriola for his excellent classes.
