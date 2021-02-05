# Rent A Car Project

Kodlama.io - Software Bootcamp.

### General info
The project consists of 4 layers: Entities, DataAccess, Business and ConsoleUI.  
For each entity (car, brand, color), there is a menu screen for related operations in ConsoleManager. Each operation is controlled by its own manager and the manager uses the Dal classes to implement crud operations. All data access layers implement generic IEntityRepository interface as a common outline.  
Project uses EntityFramework to connect to the corresponding database.  
Fluentvalidation is used to give some restrictions on the operation controls of the car entity.
#

### Technologies
Project is created with:

* Visual Studio 2012
* EntityFramework 6.4.4
* FluentValidation 7.1.1

#
