using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Serialization;
using WoodenFurnitureRestoration.API.Middleware;
using WoodenFurnitureRestoration.API.Profiles;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// ========== 1️⃣ DATABASE BAĞLANTISI ==========
builder.Services.AddDbContext<WoodenFurnitureRestorationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ========== 2️⃣ LOGGING ==========
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

// ========== 3️⃣ CORS (CORS'u SADECE BİR KEZ ekle!) ==========
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy
            .WithOrigins("https://localhost:5001")  // ✅ Blazor adresi
            .AllowAnyMethod()                       // GET, POST, PUT, DELETE
            .AllowAnyHeader()                       // Tüm headers
            .AllowCredentials();                    // Cookies izni
    });
});

// ========== 4️⃣ REPOSITORY'LER (Dependency Injection) ==========
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRestorationRepository, RestorationRepository>();
builder.Services.AddScoped<IRestorationServiceRepository, RestorationServiceRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<ISupplierMaterialRepository, SupplierMaterialRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ========== 5️⃣ CONTROLLERS ==========
builder.Services.AddControllers();
    

// ========== 6️⃣ SWAGGER/OPENAPI (API Dokümantasyonu) ==========
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wooden Furniture Restoration API",
        Version = "v1",
        Description = "Ahşap mobilya restorasyon hizmetleri API'si",
        Contact = new OpenApiContact
        {
            Name = "Support Team",
            Email = "support@example.com"
        }
    });
});

var app = builder.Build();

// ========== MIDDLEWARE CONFIGURATION ==========
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wooden Furniture API v1");
        options.RoutePrefix = string.Empty; // Swagger'ı root'ta aç
    });
}

app.UseHttpsRedirection();

// ✅ EXCEPTION HANDLING MIDDLEWARE (En başa ekle!)
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ========== CORS'u AKTİF ET (Bu SIRAda olmalı!) ==========
app.UseCors("AllowBlazor");

app.UseAuthorization();

// ========== CONTROLLERS'ı EŞLE ==========
app.MapControllers();

app.Run();