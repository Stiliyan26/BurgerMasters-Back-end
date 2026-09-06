


https://github.com/user-attachments/assets/9f6efcad-e110-467d-a5b4-62d71ebec919


# BurgerMasters - Restaurant Management System

Live: https://burgermasters.vercel.app
API: https://burgermasters-api-production.up.railway.app

## Overview

BurgerMasters is a comprehensive restaurant management system built with ASP.NET Core 6.0 that provides a complete solution for food delivery services. The application features a modern architecture with separate concerns for API, business logic, and data access layers.

### Key Features

- **User Authentication & Authorization** - JWT-based authentication with role-based access control
- **Menu Management** - Complete CRUD operations for menu items with categorization
- **Shopping Cart** - Add/remove items with real-time cart management
- **Order Processing** - Full order lifecycle from placement to completion
- **Admin Panel** - Comprehensive admin tools for menu and order management
- **Real-time Reviews** - SignalR-powered review system with live updates
- **RESTful API** - Well-documented API endpoints with Swagger integration

## Architecture

The application follows Clean Architecture principles with three main layers:

```
BurgerMasters/
├── BurgerMasters/              # Web API Layer
│   ├── Controllers/            # API Controllers
│   ├── Extensions/             # Service Registration
│   ├── Hubs/                  # SignalR Hubs
│   └── Constants/             # Validation Constants
├── BurgerMasters.Core/         # Business Logic Layer
│   ├── Contracts/             # Service Interfaces
│   ├── Models/                # DTOs and ViewModels
│   ├── Services/              # Business Logic Services
│   └── AutoMapper/            # Object Mapping Profiles
└── BurgerMasters.Infrastructure/ # Data Access Layer
    ├── Data/                  # Database Context & Models
    ├── Migrations/            # Entity Framework Migrations
    └── Common/                # Repository Pattern
```

## Technology Stack

### Backend
- **ASP.NET Core 6.0** - Web API framework
- **Entity Framework Core 6.0** - ORM for database operations
- **AutoMapper 12.0** - Object-to-object mapping
- **JWT Bearer Authentication** - Secure token-based authentication
- **SignalR** - Real-time communication for reviews
- **Swagger/OpenAPI** - API documentation
- **Microsoft.AspNetCore.Identity** - User management system

### Database
- **SQL Server** - Primary database
- **Entity Framework Migrations** - Database versioning

### Testing
- **NUnit 3.13.3** - Unit testing framework
- **Moq 4.18.2** - Mocking framework
- **FluentAssertions** - Test assertions
- **EntityFrameworkCore.InMemory** - In-memory database for testing

## Database Schema

### Core Entities

#### Users
- **ApplicationUser** - Extended Identity user with custom properties
  - Birthdate, Address
  - Cart items relationship
  - Order history

#### Menu System
- **MenuItem** - Food items with details
  - Name, Description, Price, ImageUrl
  - ItemType relationship
  - Creator tracking for admin management
- **ItemType** - Menu categories (Burgers, Sandwiches, Fries, etc.)

#### Shopping & Orders
- **ApplicationUserMenuItem** - Shopping cart items
- **Order** - Customer orders with status tracking
- **OrderDetail** - Individual items within orders

#### Reviews
- **ReviewMessage** - Customer reviews with SignalR support

## API Endpoints

### Authentication (`/api/Account`)
- `POST /Register` - User registration
- `POST /Login` - User login
- `GET /Logout` - User logout
- `POST /RefreshToken` - Token refresh

### Menu Management (`/api/Menu`)
- `GET /AllItemTypes` - Get all menu categories
- `GET /AllItemsByType` - Get items by category
- `GET /ItemDetailsById` - Get detailed item information
- `GET /SimilarProducts` - Get related items
- `GET /AllMenuItems` - Advanced menu query with filtering/sorting

### Shopping Cart (`/api/Cart`)
- `POST /AddItemToCart` - Add item to cart
- `GET /AllCartItems` - Get user's cart items
- `DELETE /RemoveCartItem` - Remove item from cart
- `DELETE /CleanUpCart` - Clear entire cart
- `GET /CartItemsCount` - Get cart item count

### Order Management (`/api/Order`)
- `POST /SentOrder` - Submit order
- `GET /AllOrdersByStatus` - Get orders by status (Admin)
- `GET /OrderById` - Get order details
- `PATCH /AcceptOrder` - Accept order (Admin)
- `PATCH /UnacceptOrder` - Unaccept order (Admin)
- `PATCH /DeclineOrder` - Decline order (Admin)
- `GET /AllOfMyOrders` - Get user's order history

### Admin Panel (`/api/Admin`)
- `POST /CreateMenuItem` - Create new menu item
- `GET /MyItemsByType` - Get admin's items by type
- `GET /CreatorItemById` - Get admin's item details
- `GET /EditItemInfo` - Get item info for editing
- `PUT /EditMenuItem` - Update menu item
- `PATCH /DeleteItem` - Delete menu item
- `GET /SimilarProductsByCreator` - Get admin's similar products

### Reviews (`/api/Review`)
- `POST /SentMessage` - Send review message
- `GET /AllMessages` - Get all review messages
- `PATCH /RemoveMessage` - Delete review message

## Security Features

- **JWT Authentication** - Secure token-based authentication
- **Role-based Authorization** - Admin and User roles
- **Input Validation** - Comprehensive model validation
- **XSS Protection** - HTML encoding for user inputs
- **CSRF Protection** - Anti-forgery token implementation
- **CORS Configuration** - Cross-origin request handling

## 🛠️ Configuration

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=BurgerMasters;Integrated Security=True;Encrypt=False"
  }
}
```

### JWT Configuration
```json
{
  "Jwt": {
    "Key": "BurgerMastersWebApiJwtSpecialKey26",
    "Issuer": "https://localhost:7129",
    "Audience": "https://localhost:7129"
  }
}
```

### CORS Policy
- Allowed Origins: `http://localhost:3001`
- Allowed Methods: All
- Allowed Headers: All
- Credentials: Enabled

## Getting Started

### Prerequisites
- .NET 6.0 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd BurgerMasters-Back-end
   ```

2. **Update connection string**
   - Modify `appsettings.json` with your SQL Server connection string

3. **Run database migrations**
   ```bash
   cd BurgerMasters/BurgerMasters
   dotnet ef database update
   ```

4. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

5. **Access the application**
   - API: `https://localhost:7129`
   - Swagger UI: `https://localhost:7129/swagger`

### Database Setup

The application uses Entity Framework Code First approach with migrations:

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## Testing

### Running Tests
```bash
cd BurgerMasters.UnitTests
dotnet test
```

### Test Coverage
- **Controller Tests** - API endpoint testing
- **Service Tests** - Business logic validation
- **Integration Tests** - End-to-end workflow testing

## Performance Features

- **Memory Caching** - Cached menu item types for improved performance
- **Pagination** - Efficient data loading with pagination support
- **Lazy Loading** - Optimized database queries
- **Connection Pooling** - Database connection optimization

## Real-time Features

### SignalR Integration
- **Review System** - Live chat-style reviews
- **Hub Configuration** - `/hubs/review` endpoint
- **Client Interface** - `IChatClient` for type-safe communication

## API Response Format

All API responses follow a consistent format:

```json
{
  "data": { /* Response data */ },
  "status": 200,
  "errorMessage": null
}
```

### Status Codes
- `200` - Success
- `400` - Bad Request
- `401` - Unauthorized
- `404` - Not Found
- `409` - Conflict
- `422` - Unprocessable Entity
- `500` - Internal Server Error

## Frontend Integration

The API is designed to work with React frontend applications:

- **CORS Enabled** - Cross-origin requests supported
- **JSON Serialization** - Nested collections included
- **Authentication Headers** - JWT token support
- **Error Handling** - Consistent error responses

## Development Guidelines

### Code Organization
- **Repository Pattern** - Data access abstraction
- **Service Layer** - Business logic separation
- **DTO Pattern** - Data transfer objects
- **AutoMapper** - Object mapping
- **Dependency Injection** - Service registration

### Validation
- **Model Validation** - Data annotation attributes
- **Custom Validation** - Business rule validation
- **Error Constants** - Centralized error messages

## Deploy (Railway or Render)

Vercel cannot host this ASP.NET API. Push to `main` deploys when the repo is linked.

1. Create a **Neon** Postgres database (separate from any Next.js/Prisma DB).
2. Set env vars from `.env.example` — especially `ConnectionStrings__DefaultConnection` (use Neon **direct / unpooled** host) and `CORS_ORIGINS` (your Vercel frontend URL).
3. Host with Docker (`Dockerfile` at repo root):
   - **Railway**: New Project → Deploy from GitHub → `Stiliyan26/BurgerMasters-Back-end` → production branch `main`.
   - **Render**: New Web Service → same repo, or apply `render.yaml`.
4. Startup runs `Database.Migrate()`, which applies the Postgres migration and seeds menu items + admin users.

Seed admins (original): `stiliyan@gmail.com` / `Admin#123` and `peter@gmail.com` / `Admin#123`.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request





