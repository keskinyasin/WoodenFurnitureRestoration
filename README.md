# WoodenFurnitureRestoration

A comprehensive furniture restoration management system built with .NET 8 and Blazor.

## Features

### Restoration Project Management (Proje Yönetimi)

The system allows you to manage furniture restoration projects with full CRUD operations:

#### How to Delete a Restoration Project (Proje Nasıl Silinir)

1. Navigate to the Admin Panel at `/admin/restorations`
2. Find the restoration project you want to delete in the list
3. Click the **trash icon (🗑️)** button in the "İşlemler" (Actions) column
4. A confirmation dialog will appear asking "Bu restorasyonu silmek istediğinizden emin misiniz?"
5. Click the confirmation button to permanently delete the project
6. The project will be soft-deleted (marked as deleted but not removed from database)

#### Other Operations

- **View (Görüntüle)**: Click the eye icon (👁️) to view project details
- **Edit (Düzenle)**: Click the pencil icon (✏️) to edit project information
- **Create (Yeni Ekle)**: Click the "Yeni Restorasyon" button to add a new restoration project

### Project Structure

- **WoodenFurnitureRestoration.Entity**: Domain entities
- **WoodenFurnitureRestoration.Data**: Data access layer with repositories
- **WoodenFurnitureRestoration.Core**: Business logic and services
- **WoodenFurnitureRestoration.API**: RESTful API endpoints
- **WoodenFurnitureRestoration.Blazor**: Blazor web UI
- **WoodenFurnitureRestoration.Shared**: Shared DTOs and models

## Technology Stack

- .NET 8
- Blazor Server
- Entity Framework Core
- AutoMapper
- Bootstrap 5

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (or compatible database)

### Running the Application

1. Clone the repository
2. Update database connection string in `appsettings.json`
3. Run database migrations
4. Start the API project: `dotnet run --project WoodenFurnitureRestoration.API`
5. Start the Blazor project: `dotnet run --project WoodenFurnitureRestoration.Blazor`

## API Endpoints

### Restorations

- `GET /api/restorations` - Get all restoration projects
- `GET /api/restorations/{id}` - Get a specific restoration project
- `POST /api/restorations` - Create a new restoration project
- `PUT /api/restorations/{id}` - Update a restoration project
- `DELETE /api/restorations/{id}` - Delete a restoration project (soft delete)