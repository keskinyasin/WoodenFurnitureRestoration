using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WoodenFurnitureRestoration.Data.DbContextt;

namespace WoodenFurnitureRestoration.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WoodenFurnitureRestorationContext>
{
    public WoodenFurnitureRestorationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WoodenFurnitureRestorationContext>();

        // Connection string'inizi buraya yazın
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=WoodenFurnitureRestoration;Trusted_Connection=True;TrustServerCertificate=True;");

        return new WoodenFurnitureRestorationContext(optionsBuilder.Options);
    }
}