Welcome to OpenERP

This project is an attempt at an Enterprise Resource Planning system, written primarily using ASP.NET Core and Entity Framework. Standard front end is delivered via Razor Pages/Views, but the system will be developed with as little coupling between front end and back end as possible within an MVC pattern. This is so the front end can be modified and customised to the taste of any future projects.

It is not my ambition to develop a Single Page Application for this system, but to focus more on the server and database design side. The standard front end provided is for basic functionality usage and testing, and you are encouraged to expand it or develop your own. As the project grows, the front end will of course be developed, but the focus is on getting some of the design and structure of the server and database built up to begin with.

Please be sure to read the License file (BSD License) before use.

Installation steps:-

First off, create a json file in the repo root directory, config.json. Copy and paste the below into it. 

{
    "ConnectionStrings" : {
        "OpenERPContextDb": "Server=<database_hostname>;Database=<database_name>;Uid=<database_user>;Pwd=<databse_user_password>"
    }
}

Set the value of "OpenERPContextDb" to a connection string for the relational database system you'd like to use.

Note: You can change your desired database system by altering the method call in OpenERPContext.cs under the ErpDbContext project.

Then build the solution: dotnet build

Create a database migration: dotnet ef migrations add InitialCreate

Create the database schema: dotnet ef database update

Populate the database with seed data: dotnet run /seed

Run the application using Kestrel: dotnet run

Note: the default initial username is manager@openerp.com, and the password is P455@w0rd!