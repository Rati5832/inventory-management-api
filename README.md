# Inventory Management API

Inventory Management API is a .NET 8 REST API for managing products, categories, warehouses, suppliers, stock levels, stock movements, and product files.

The solution follows Clean Architecture and uses CQRS with MediatR to keep HTTP, application, domain, and persistence concerns separated. Inventory operations support stock receipt, dispatch, transfer, manual adjustment, low-stock reporting, movement history, and optimistic concurrency control.

## Features

- Create, read, update, and delete categories, products, warehouses, and suppliers
- Search products by name or SKU
- Filter products by category, active status, and price range
- Sort and paginate product results
- Assign suppliers to products and remove product-supplier relationships
- Track product quantities independently in each warehouse
- Receive stock into a warehouse
- Dispatch stock while preventing negative inventory
- Transfer stock between warehouses
- Adjust stock to a counted quantity and record the previous and new values
- Browse all stock records, low-stock products, and movement history
- Upload, list, download, and delete product files
- Validate file size, extension, and content type
- Prevent conflicting stock updates with SQL Server row-version concurrency
- Validate application requests with FluentValidation
- Return centralized Problem Details responses for application errors
- Write structured request and application logs with Serilog
- Explore and call the API through Swagger UI in development
- Verify core application behavior with xUnit unit tests

## Technology stack

- .NET 8 and C#
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server / SQL Server Express
- Clean Architecture
- CQRS and MediatR
- FluentValidation
- Mapster
- Serilog
- Swagger / OpenAPI
- xUnit, Moq, and FluentAssertions

## Domain model

The main relationships are:

```text
Category 1 --- * Product
Product  * --- * Supplier
Product  1 --- * ProductFile
Product  1 --- * Stock * --- 1 Warehouse
Stock    1 --- * StockMovement
```

A stock record represents the quantity of one product in one warehouse. The database prevents duplicate stock records for the same product and warehouse combination.

## Stock workflow

Every quantity change creates a stock movement:

- `StockIn` increases the quantity in a warehouse.
- `StockOut` decreases the quantity and rejects requests that exceed available stock.
- `Transfer` performs a stock-out from the source warehouse and a stock-in into the destination warehouse.
- `Adjustment` replaces the current quantity with a counted quantity and records both the previous and new values.

Low-stock results contain stock records whose quantity is less than or equal to the product's reorder level. SQL Server row-version tokens protect stock updates from being silently overwritten by concurrent requests. Conflicts are returned as HTTP `409 Conflict`.

## Product files

Product files are stored on the local file system, while their metadata is stored in SQL Server. The default storage directory is `uploads/products` under the API content root.

Accepted file types:

- JPEG
- PNG
- WEBP
- PDF

The maximum file size is 5 MB. Stored paths are validated before files are opened or deleted to prevent access outside the configured storage directory. Uploaded files and generated logs are excluded from Git.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server Express
- EF Core CLI tools

Install the EF Core CLI if it is not already available:

```powershell
dotnet tool install --global dotnet-ef --version 8.*
```

## Local setup

Clone the repository and restore the packages:

```powershell
git clone https://github.com/Rati5832/inventory-management-api.git
cd inventory-management-api
dotnet restore InventoryManagement.sln
```

Store your local SQL Server connection string with .NET User Secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\SQLEXPRESS;Database=InventoryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;" --project src/InventoryManagement.API
```

Replace `Server=.\SQLEXPRESS` if your SQL Server instance has a different name. User Secrets are stored outside the repository and are not committed to Git.

Apply the included migrations:

```powershell
dotnet ef database update --project src/InventoryManagement.Infrastructure --startup-project src/InventoryManagement.API
```

If the local HTTPS development certificate is not trusted, run:

```powershell
dotnet dev-certs https --trust
```

## Run the API

Start the API with its HTTPS launch profile:

```powershell
dotnet run --project src/InventoryManagement.API --launch-profile https
```

Development URLs:

- API: `https://localhost:7243`
- Swagger UI: `https://localhost:7243/swagger`

Swagger opens automatically when the API is started through the HTTPS launch profile.

## API endpoints

### Categories

| Method | Route | Purpose |
| --- | --- | --- |
| `GET` | `/api/categories` | List categories |
| `GET` | `/api/categories/{id}` | Get a category |
| `POST` | `/api/categories` | Create a category |
| `PUT` | `/api/categories/{id}` | Update a category |
| `DELETE` | `/api/categories/{id}` | Delete a category |

### Products

| Method | Route | Purpose |
| --- | --- | --- |
| `GET` | `/api/products` | Search, filter, sort, and paginate products |
| `GET` | `/api/products/{id}` | Get a product |
| `POST` | `/api/products` | Create a product |
| `PUT` | `/api/products/{id}` | Update a product |
| `DELETE` | `/api/products/{id}` | Delete a product |
| `POST` | `/api/products/{productId}/suppliers/{supplierId}` | Assign a supplier |
| `GET` | `/api/products/{productId}/suppliers` | List assigned suppliers |
| `DELETE` | `/api/products/{productId}/suppliers/{supplierId}` | Remove a supplier |
| `POST` | `/api/products/{productId}/files` | Upload a product file |
| `GET` | `/api/products/{productId}/files` | List product files |
| `GET` | `/api/products/{productId}/files/{fileId}/download` | Download a product file |
| `DELETE` | `/api/products/{productId}/files/{fileId}` | Delete a product file |

Product list query parameters include `pageNumber`, `pageSize`, `search`, `categoryId`, `isActive`, `minPrice`, `maxPrice`, `sortBy`, and `sortDirection`.

### Warehouses

| Method | Route | Purpose |
| --- | --- | --- |
| `GET` | `/api/warehouses` | List warehouses |
| `GET` | `/api/warehouses/{id}` | Get a warehouse |
| `POST` | `/api/warehouses` | Create a warehouse |
| `PUT` | `/api/warehouses/{id}` | Update a warehouse |
| `DELETE` | `/api/warehouses/{id}` | Delete a warehouse |

### Suppliers

| Method | Route | Purpose |
| --- | --- | --- |
| `GET` | `/api/suppliers` | List suppliers |
| `GET` | `/api/suppliers/{id}` | Get a supplier |
| `POST` | `/api/suppliers` | Create a supplier |
| `PUT` | `/api/suppliers/{id}` | Update a supplier |
| `DELETE` | `/api/suppliers/{id}` | Delete a supplier |

### Stock

| Method | Route | Purpose |
| --- | --- | --- |
| `GET` | `/api/stocks` | List stock records |
| `GET` | `/api/stocks/low-stock` | List low-stock products |
| `GET` | `/api/stocks/{id}` | Get a stock record |
| `GET` | `/api/stocks/{id}/movements` | List movement history for a stock record |
| `POST` | `/api/stocks/stock-in` | Receive stock |
| `POST` | `/api/stocks/stock-out` | Dispatch stock |
| `POST` | `/api/stocks/transfer` | Transfer stock between warehouses |
| `PATCH` | `/api/stocks/{id}/adjust` | Set a counted stock quantity |
| `GET` | `/api/stock-movements` | List all stock movements |

## Validation and error handling

FluentValidation runs through a MediatR pipeline before request handlers execute. Validation failures and application exceptions are converted into consistent RFC 7807 Problem Details responses.

Common response codes:

- `200 OK` for successful reads and updates
- `201 Created` for newly created resources
- `204 No Content` for successful deletions
- `400 Bad Request` for validation failures
- `404 Not Found` when a requested resource does not exist
- `409 Conflict` for duplicate data, invalid inventory state, or concurrency conflicts

## Logging

Serilog records HTTP requests, application requests, and unhandled exceptions. Logs are written to the console and to daily rolling files under `src/InventoryManagement.API/logs` when the API is launched from the repository root. File logs are retained for 14 days and are ignored by Git.

## Build and test

Build the entire solution:

```powershell
dotnet build InventoryManagement.sln
```

Run the unit tests:

```powershell
dotnet test InventoryManagement.sln
```

The current test suite covers stock receipt, stock dispatch, stock adjustment, supplier assignment, and product file validation.

## Configuration

The required configuration key is:

- `ConnectionStrings:DefaultConnection`

The optional file storage setting is:

- `FileStorage:ProductFilesPath`

For local development, use .NET User Secrets. In deployed environments, use an environment variable or a managed secret store. The environment-variable form of the connection string key is `ConnectionStrings__DefaultConnection`.
