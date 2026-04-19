# EnverSoft Mini Project - Supplier Portal

This is a 3-tier proof of concept for the EnverSoft Software Developer mini project.

## What the POC does
- Provides a screen to capture a **company name** and **telephone number**
- Provides a search screen to look up a supplier by **company name**
- Saves data to a **Microsoft SQL Server** database
- Uses a **3-tier architecture**:
  - **Front Layer**: ASP.NET Core MVC web app
  - **Services Layer**: application/service layer with validation and business rules
  - **Repository Layer**: EF Core repository against SQL Server

## Tech stack
- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- HTML / CSS

## Solution structure
- `src/SupplierPortal.Web` - UI / presentation layer
- `src/SupplierPortal.Application` - service layer
- `src/SupplierPortal.Domain` - entities and contracts
- `src/SupplierPortal.Infrastructure` - EF Core DbContext and repositories
- `database` - SQL scripts to create and seed the database

## Database setup
1. Open SQL Server Management Studio
2. Run `database/01_CreateDatabase.sql`
3. Confirm that the `EnverSoftSuppliersDb` database and `Suppliers` table were created
4. Update the connection string in `src/SupplierPortal.Web/appsettings.json` if your SQL Server instance differs

## Run the application
1. Open the solution in Visual Studio 2022
2. Restore NuGet packages
3. Build the solution
4. Set `SupplierPortal.Web` as the startup project
5. Run the project

## Notes for assessor
- The original supplier spreadsheet provided by EnverSoft was converted into seed SQL data
- New suppliers are assigned the next available supplier code inside the repository
- Duplicate company names are blocked in the service layer and database layer
- This POC intentionally excludes authentication as requested in the brief

## Suggested GitHub repo name
`enversoft-supplier-portal-poc`
