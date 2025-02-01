
# DVDRentalStoreManagementAPI

DVDRentalStoreManagementAPI is a RESTful API built using ASP.NET Core to manage the backend operations of a DVD rental store. The API provides functionality for handling DVD inventory, customer data, rental processes, and returns.


## Features
- DVD Management: Add, update, delete, and list DVDs.
- Customer Management: Add, update, and list customer details.
- Rental Management: Create, return, and track rental transactions.
- Search and Filtering: Search DVDs by attributes like title, genre, and availability.


## Prerequisites
Make sure you have the following installed:

- .NET SDK 
- Visual Studio 
- SQL Server


## Installation

Clone the repository:

```bash
  git clone https://github.com/JanusikaUT/DVDRentalStoreManagementAPI.git

```
Open the project in Visual Studio:
- Launch Visual Studio.
- Open the solution file (DVDRentalStoreManagementAPI.sln) from the cloned repository.

Restore dependencies:

- Visual Studio will automatically restore the required NuGet packages. If not, you can manually restore them by navigating to Tools > NuGet Package Manager > Restore NuGet Packages.

Configure the database:

- Open appsettings.json (or another configuration file) and configure your database connection string in the "ConnectionStrings" section.
```bash
  "ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DVDRentalStore;Trusted_Connection=True;"
}
```  
Run the project:

- Press F5 or click on Start Debugging in Visual Studio to build and run the API.
- The API should be running at http://localhost:5000 or https://localhost:5001 by default.
