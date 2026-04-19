# Architecture Summary

## Why this design
The project brief requires a real database and a middle/services layer. To satisfy that requirement clearly, I used a 3-tier design:

1. **Presentation Layer**
   - ASP.NET Core MVC UI
   - Handles user input and page rendering

2. **Service Layer**
   - Validates incoming data
   - Handles business rules such as duplicate supplier prevention

3. **Repository/Data Layer**
   - EF Core repository
   - SQL Server database access

## Main flow
1. User enters supplier information in the web page
2. `SuppliersController` sends the request to `ISupplierService`
3. `SupplierService` validates the request and checks for duplicates
4. `ISupplierRepository` persists data using `AppDbContext`
5. Search requests follow the same path in reverse until the result is displayed

## Assumptions
- Company name is unique for this POC
- Search is exact-match by company name to keep the first version simple
- Authentication is out of scope per the brief
