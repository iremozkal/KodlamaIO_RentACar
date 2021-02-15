# Rent A Car Project

Kodlama.io - Software Bootcamp.

### General info
The project consists of 5 layers: Entities, DataAccess, Business, Core, ConsoleUI and WebAPI.  
For each entity (car, brand, color, customer, user, rental), there is a menu screen for related operations in ConsoleManager. Each operation is controlled by its own manager and the manager uses the Dal classes to implement crud operations. All data access layers implement generic IEntityRepository interface as a common outline.  
WebAPI layer is created as a ASP.NET WebAPI 2 Project. Unity has been used for dependency injection. UnityResolver class has been created for resolving dependencies. In WebApiConfig class, UnityContainer has been initialised and dependency resolver has been set. Controllers in WebApi layer inherits ApiContoller class. By default it has access to some routing rules for get, post, put and delete operations.  
EntityFramework is used to connect to the corresponding database.  
Fluentvalidation is used to give some restrictions on the operation controls of the car entity.  
#

### Technologies
Project is created with:

* .Net Framework 4.5
* Visual Studio 2012
* EntityFramework 6.4.4
* FluentValidation 7.1.1
* Unity 5.7.3 

### Other Camp Works
[repl.it/@iiremozkal](https://repl.it/@iiremozkal)
