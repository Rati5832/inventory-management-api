# Inventory Management API

> Work in progress

Inventory Management API is a .NET 8 backend project for managing products, categories, warehouses, suppliers, and warehouse stock.

The project follows Clean Architecture and uses CQRS with MediatR to separate read and write operations.

## Implemented features

- Category CRUD operations
- Product CRUD operations
- Product pagination
- Search by product name or SKU
- Filtering by category, status, and price range
- Sorting by name or price
- Request validation with FluentValidation
- DTO mapping with Mapster
- Centralized exception handling
- Entity Framework Core configurations and migrations
- Swagger/OpenAPI documentation

## Planned features

- Warehouse CRUD operations
- Supplier management
- Product-supplier relationships
- Stock-in and stock-out operations
- Inventory adjustments
- Stock movement history
- Product file upload, download, and deletion
- Structured logging with Serilog
- Unit tests with xUnit
- Complete project documentation

## Technology stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture
- CQRS and MediatR
- FluentValidation
- Mapster
- Serilog
- Swagger/OpenAPI
- xUnit, Moq, and FluentAssertions

## Solution structure

- `InventoryManagement.Domain` — entities, enums, and domain models
- `InventoryManagement.Application` — commands, queries, handlers, DTOs, validators, mappings, and abstractions
- `InventoryManagement.Infrastructure` — EF Core persistence, repositories, configurations, and migrations
- `InventoryManagement.API` — controllers, HTTP pipeline, Swagger, and exception handling
- `InventoryManagement.UnitTests` — automated tests

## Build

```powershell
dotnet build InventoryManagement.sln
