# WebApi Project Structure

## Multi-Layer Architecture

- **Data Access Layer**
  - *ShiftsLogger.DAL*
  - Contains DbContext, entity models, migrations and EF Core configuration files
  - EF Core Configuration
    - Fluent API allows configuration to be specified without modifying entity classes
    - Configurations are extracted to a separate class and loaded with assembly scanning

- **Repository Layer**
  - *ShiftsLogger.Repository* project contains classes for database communication
  - *ShiftsLogger.Repository.Common* project contains interfaces used by dependency injection
  - References *ShiftsLogger.DAL* for obtaining database interaction methods
  - References *ShiftsLogger.Repository.Common* for interface implementation

- **Service Layer**
  - *ShiftsLogger.Service* project has classes which contain business logic
  - *ShiftsLogger.Service.Common* project contains interfaces used by dependency injection
  - References *ShiftsLogger.Service.Common* for interface implementation
  - References *ShiftsLogger.Repository.Common* for dependency injection

- **WebApi Layer**
  - *ShiftsLogger.WebApi* contains REST models and controller classes which handle HTTP requests and responses
  - References *ShiftsLogger.Service.Common* for dependency injection

- **Models Layer**
  - *ShiftsLogger.Models* defines models used in the application
  - Can be referenced by any projects that use the models

- **Common Layer**
  - *ShiftsLogger.Common* contains generic and extension methods, validation, constants, etc.
  - Can be referenced by any project

## Dependency Injection

- Autofac modules are used for registering layer components
- Data Access, Repository and Service layer each contain a module where classes are registered to the Autofac IoC container through their interfaces
- *ShiftsLogger.Root* project acts as a thin layer that composes modules. It serves as the composition root, handling the registration of all modules without exposing the implementation details to the WebApi project.

## Model types

- **Entity models**
  - located in DAL project
  - represent database tables
  - used for database configuration and interaction
- **DTOs**
  - located in Models project
  - models used inside services for business logic
- **REST models**
  - located in WebApi project
  - format the data exposed to clients that consume the API
  - allow modification of internal model types without impacting API consumers
