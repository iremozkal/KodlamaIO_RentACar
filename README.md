# Rent A Car Project

Kodlama.io - Software Bootcamp.

### General info
The project consists of 5 layers: Entities, DataAccess, Business, Core, ConsoleUI and WebAPI.  
For each entity (car, brand, color, customer, user, rental), there is a menu screen for related operations in ConsoleManager. Each operation is controlled by its own manager and the manager uses the Dal classes to implement crud operations. All data access layers implement generic IEntityRepository interface as a common outline.  
Web Api layer is created as a ASP.NET WebAPI 2 Project. Unity has been used for dependency injection. UnityResolver class registers the type-mapping with the container, 
so that it can create the correct object for the given type. After that, it automatically injects the dependencies using the resolve() method. Web Api Layer resolves the dependency of the Business layer in WebApiConfig class with setting of the dependency resolver. Controllers in Web Api layer inherits ApiController class. By default it has access to some routing rules for get, post, put and delete operations.  
EntityFramework is used to connect to the corresponding database.  
Fluentvalidation is used to give some restrictions on the operation controls of the car entity.  
#

### Technologies
Project is created with:

* Visual Studio 2012
* .Net Framework 4.5
* EntityFramework 6.4.4
* FluentValidation 7.1.1
* Unity 5.7.3 

### Other Camp Works
[repl.it/@iiremozkal](https://repl.it/@iiremozkal)
