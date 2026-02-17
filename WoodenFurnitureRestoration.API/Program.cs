using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WoodenFurnitureRestoration.API.Profiles;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Core.Services.Concrete;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// ========== 1️⃣ DATABASE ==========
builder.Services.AddDbContext<WoodenFurnitureRestorationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

// ========== 2️⃣ AUTOMAPPER ==========
builder.Services.AddAutoMapper(
    typeof(MappingProfile).Assembly,
    typeof(WoodenFurnitureRestoration.Shared.Mappings.SupplierMappingProfile).Assembly
);

// ========== 3️⃣ LOGGING ==========
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

// ========== 4️⃣ CORS ==========
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy
            .WithOrigins("https://localhost:7265", "https://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// ========== 5️⃣ UNİTOFWORK (Tek kayıt yeterli!) ==========
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ========== 6️⃣ CORE SERVİCE'LER ==========
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRestorationService, RestorationService>();
builder.Services.AddScoped<IRestorationServiceService, RestorationServiceService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplierMaterialService, SupplierMaterialService>();
builder.Services.AddScoped<ITagService, TagService>();

// ========== 7️⃣ CONTROLLERS ==========
builder.Services.AddControllers();

// ========== 8️⃣ SWAGGER ==========
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wooden Furniture Restoration API",
        Version = "v1",
        Description = "Ahşap mobilya restorasyon hizmetleri API'si"
    });
});

var app = builder.Build();

// ========== MIDDLEWARE ==========
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wooden Furniture API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseAuthorization();
app.MapControllers();

app.Run();