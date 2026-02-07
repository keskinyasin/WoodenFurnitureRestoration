using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WoodenFurnitureRestoration.API.Profiles;
using WoodenFurnitureRestoration.Blazor.Components;
using WoodenFurnitureRestoration.Blazor.Services;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Core.Services.Concrete;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// ========== DATABASE ==========
builder.Services.AddDbContext<WoodenFurnitureRestorationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

// ========== AUTOMAPPER ==========
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ========== AUTHENTICATION ==========
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/admin");
        o.LogoutPath = new PathString("/logout");
        o.AccessDeniedPath = new PathString("/access-denied");
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// ========== LOGGING ==========
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

// ========== UNİTOFWORK ==========
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ========== CORE SERVİCE'LER ==========
builder.Services.AddScoped<IAddressService, WoodenFurnitureRestoration.Core.Services.Concrete.AddressService>();
builder.Services.AddScoped<IBlogPostService, WoodenFurnitureRestoration.Core.Services.Concrete.BlogPostService>();
builder.Services.AddScoped<ICategoryService, WoodenFurnitureRestoration.Core.Services.Concrete.CategoryService>();
builder.Services.AddScoped<ICustomerService, WoodenFurnitureRestoration.Core.Services.Concrete.CustomerService>();
builder.Services.AddScoped<IInventoryService, WoodenFurnitureRestoration.Core.Services.Concrete.InventoryService>();
builder.Services.AddScoped<IInvoiceService, WoodenFurnitureRestoration.Core.Services.Concrete.InvoiceService>();
builder.Services.AddScoped<IOrderService, WoodenFurnitureRestoration.Core.Services.Concrete.OrderService>();
builder.Services.AddScoped<IOrderDetailService, WoodenFurnitureRestoration.Core.Services.Concrete.OrderDetailService>();
builder.Services.AddScoped<IPaymentService, WoodenFurnitureRestoration.Core.Services.Concrete.PaymentService>();
builder.Services.AddScoped<IProductService, WoodenFurnitureRestoration.Core.Services.Concrete.ProductService>();
builder.Services.AddScoped<IRestorationService, WoodenFurnitureRestoration.Core.Services.Concrete.RestorationService>();
builder.Services.AddScoped<IRestorationServiceService, WoodenFurnitureRestoration.Core.Services.Concrete.RestorationServiceService>();
builder.Services.AddScoped<IReviewService, WoodenFurnitureRestoration.Core.Services.Concrete.ReviewService>();
builder.Services.AddScoped<IShippingService, WoodenFurnitureRestoration.Core.Services.Concrete.ShippingService>();
builder.Services.AddScoped<ISupplierService, WoodenFurnitureRestoration.Core.Services.Concrete.SupplierService>();
builder.Services.AddScoped<ISupplierMaterialService, WoodenFurnitureRestoration.Core.Services.Concrete.SupplierMaterialService>();
builder.Services.AddScoped<ITagService, WoodenFurnitureRestoration.Core.Services.Concrete.TagService>();

// ========== CONTROLLERS (API) ==========
builder.Services.AddControllers();

// ========== SWAGGER (YENİ EKLENDİ!) ==========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wooden Furniture Restoration API",
        Version = "v1",
        Description = "Ahşap mobilya restorasyon hizmetleri API'si"
    });
});

// ========== HTTPCLIENT ==========
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7265")
    }
);

// ========== BLAZOR SERVİCE'LER ==========
builder.Services.AddScoped<AdminSessionService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.AddressService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.BlogPostService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.CategoryService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.CustomerService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.InventoryService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.InvoiceService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.OrderService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.OrderDetailService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.PaymentService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.ProductService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.RestorationService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.RestorationServiceService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.ReviewService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.ShippingService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.SupplierService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.SupplierMaterialService>();
builder.Services.AddScoped<WoodenFurnitureRestoration.Blazor.Services.TagService>();

// ========== RAZOR COMPONENTS ==========
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ========== MIDDLEWARE ==========
if (app.Environment.IsDevelopment())
{
    // ✅ SWAGGER (Development'ta aktif)
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wooden Furniture API v1");
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();