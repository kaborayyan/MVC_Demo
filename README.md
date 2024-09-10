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
* Use the next code to activate all Model Configuration Classes  
```C#
modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
```

### Business Logic Layer
* A Repository for each Class  
* Create two folders Repository and Interface inside The BLL
* Create an interface for each Model Class, the interface will carry the signatures of Methods
* Add a reference for DAL inside the BLL
* You have to make all your Classes public to be able to use them
* Create an interface for each to be built Repository
* Five Basic Methods to create their signature: GetAll(), GetById(), Create(), Update() and Delete()
* Create a Repository Class for each Model and let it implement the already created Interface
* Instead of opening and closing the connection each time
* Create a private field of type AppDbContext you created before
* You have a create a constructor to asign value to the field

### Dependecy Injection
* An Object from a Class that needs an Object from another Class to be created
* You create your dependecy injection in Program in your presentation layer
* This means CLR can create an object from AppDbContext at any time
* Don't forget to add reference from BLL which already has a reference from

### Back to Data Access Layer
* Create a constructor for AppDbContext
* Use the overload with DbContextOptions
* Edit the dependency injection of AddDbContext to insert the connection string there
* Delete the OnCofigure

### appsettings.json
* This's the best place to keep the connection string
* Why?
* All the C# files will be compiled to .dll files that we won't be able to edit
* So keep your connection string there and send the key to the AppDbContext dependency injector

### To do the Migrations
* You need to install EFCore tools in your presentation layer, where you Main() function exists
* Choose you DAL as your default project for migrations
```
Add-Migration "InitialCreate" -OutputDir Data/Migrations
```
* The prvious line will specify the new folder that will be created

### Adding New Controllers
* For each controller you add, create its default index view
* You have to create a private field from the repository in the controller to able to use its methods
* Modify the contructor to assign default value for the private field instead of null
* Allow dependency injection in Program.cs for the repository and its interface classes
* The GetAll() method shoud return IEnumerable type of data
* Move the use name space to the view import
* Modify the _Layout view to add links to the new page

### The View
* To be able to use the incoming object from the Controller
* Use model with lowercase m at the start of page
```
@model IEnumerable<Department>
```
* However inside the code use Model with upper case m
* Adjust the form and use the html helpers and tag helpers
* You can use disabled or readonly to disable the html form fields