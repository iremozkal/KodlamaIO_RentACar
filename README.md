# Rent A Car Project

Kodlama.io - Software Bootcamp.

### General info
The project consists of 5 layers: Entities, DataAccess, Business, ConsoleUI and Core.  
For each entity (car, brand, color, customer, user), there is a menu screen for related operations in ConsoleManager. Each operation is controlled by its own manager and the manager uses the Dal classes to implement crud operations. All data access layers implement generic IEntityRepository interface as a common outline.  
Project uses EntityFramework to connect to the corresponding database.  
Fluentvalidation is used to give some restrictions on the operation controls of the car entity.
#

### Technologies
Project is created with:

* Visual Studio 2012
* EntityFramework 6.4.4
* FluentValidation 7.1.1

### Other Camp Works
[repl.it/@iiremozkal](https://repl.it/@iiremozkal)
