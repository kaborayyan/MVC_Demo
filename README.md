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

### The Edit "Update" & "Delete" Methods
* Just like the details
* You have to create two codes for the HttpGet and HttpPost
* Since the code is the same in the HttpGet method
* You can call the Details() method inside the Edit() and Delete() methods
* The view() has an overload that accept a view name, use it to redirect the result to another view

### The Employee
* Create the model
* Create The model Configuration Class
* Add to the DbContext
* Create the IRepository interface then the Repository as before
* Implement all methods

### Generic Interface & Repository
* You can let the repositories implement the IGenericRepository directly
* However it's better to leave it like this in case you needed to add something special to each repository for example
* For the generic repository, you have to put constraints on the data type
* To do this create a base class model that both department and employee will inherit from it
* Thus the repository will accept data types from any class that inherit from the base model
* Use Set<T>() to replace the data type "Class"
* Let each repository inherit from the Generic Repository and the corresponding interface

### Employee Controller and Views
* Follow same steps as Department
* Write the script codes for JQuery validation in a separate section in the create html page
* So that it will work after loading the JQuery library

### To send extra data from Action to View
* Use View's Dictionary
* All inherited from Controller
* You can create a div on the frontend and display the message there
    * ViewData - Dictionary - Require Casting
    ```C#
    string Message = "Hello world";
    ViewData["Message"] = Message + "From Viewdata";
    ```
    * ViewBag - Dynamic
    ```C#
    ViewBag.Message = Message + "From ViewBag";
    ```
    * TempData - Dictionary - Require Casting
    * Used to transfer data from request to another
    ```C#
    TempData["Message"] = Message + "From Viewdata";
    ```

### Add the relation between Department and Employees
* The department will have a ICollection property of The employee
* The employee will have property of type Department
* Better to do it through Data Annotation or Class Configuration
* Go to the Create view and add the needed input
* For this you need the Controller to accept data from Department Repository
* Add it as a private field and to the constructor
* Modify the Create and Edit method to display the departments
* Use ViewData to send the extra data to the View "To the partial view"
* Modify the Index view to add the new field
* EF doesn't load any navigational property by default
* So modify the repository to load the navigation property

### To add a Search Function
* First add it to the target IRepository
* Modify the Repository and implement the new changes
* Modify the private property in the Generic Repository to private protected to be able to use it in the inherited Classes
* Modify the Index Action in the Employee Controller to implement the function
* Remove the HttpGet since the action will act as Get and Post
* Modify the Index view and put the needed fields

### About Dependecy Injection
* Dependency Injection is to allow CLR to create Object from any Class and inject it to another Class
* We have three Methods
```C#
// In Program.cs
builder.Services.AddScoped
builder.Services.AddTransient
builder.Services.AddSingleton
```
* The difference is how long the object will stay in the memory
    * AddScoped: lifetime per request
    * AddTransient: lifetime per operation
    * AddSingleton: lifetime per application
* Look into folder Services in the presentation services to check created Interfaces and Classes for testing
* Go to the home controller and modify the constructor
* Go to Program.cs and do the necessary dependency injections
* Run the app and check the differences
    * Singleton is the same since the app is running
    * Transient both variables guid are different will create many objects as you need
    * Scoped are the same object whatever how many objects you ask
* DbContext and Repository are best with Scoped
* Cashing is better with Singleton

### ViewModel
* Not all the data in the database need to be sent to the view
* To avoid sending sensitive data you use a ViewModel as a bridge between the model "the database" and the view
* Create EmployeeViewModel in the ViewModels Folder
* Go to the controller and refactor the code
* But you will need to cast the object from the ViewModel Class to the normal Model Class
* This is called Maping which is of two types
    * Manual "time consuming" -- Check Session05 Video 07
    * Auto "using auto mapper"
* To use Automapper in the presentation layer, from the dependecies use the nuget package manager to install the automapper
* Create a folder called mapping and inside a folder for each class
* Check Session05 and Video08
* Create ClassProfile for each Class Model and inherit Profile from mapper
* Create method CreateMap inside the default constructor
* Modify the contructor of the target controller
* Modify the Index and Create methods
* Change the Model type in the Views
* Modify the edit and delete methods like same
* Do not forget to allow the dependecy injection for AutoMapper

### Unit of Work: Design Pattern
* If you need to do multiple injections from several repositories
* In the Business Logic Layer
* Create an interface for the Unit Of Work
* In the interface, inlcude signature for the 
* Create Class UnitOfWork and inherit the interface then implement it
* Modify the Employee Controller, let it use the Unit of work instead of the Employee and department repository
* Do Not Forget the dependency injection