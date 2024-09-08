# Company.MVC.Solution

### 3 Layer Architecture
1. Data Access Layer
    1. Deals with database
    2. A Class Library
    3. Includes Models DbContext Migrations

2. Business Logic Layer
    1. Deals with Business
    2. 2 Design Patterns
        1. Generic Repository
        2. Unit of Work

3. Presentation Layer
    1. MVC
    2. Web API
    3. Desktop
    4. Mobile

### Onion Layer Architecture
1. Domsin Model Layer
    Class Represent Table in dataase

2. Repository Layer
    DbContext
    Repository

3. Services Layer
    Rest of business

4. Presentation Layer

### Create New MVC Project with .NET 8.0
* Project should be named Project.Client  
* Solution should be named Project.Client Solution
* The default project has several default files: ErrorModel for client side error, Home Controller, default Views for home. BootStrap and JQuery are included and enabled by default by default

### Start with Data Access Layer
* Create a Class Library for the same solution
* Create Folder: Models and Data
* Inside Data create two more folders: Configuration and Context
* From Depedencies install NuGet EntityFrameworkCore SQL server
* Under Context create AppDbContext
* Override OnConfiguring
* Create the Connection String
* Add DbSet for each Class in the Models
* Create Configuration files for each Model
* Implement the IEntityTypeConfiguration<> Interface
* In AppDbContext override OnModelCreating
* Use
```C#
modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
```
to activate all Model Configuration Classes
* 
