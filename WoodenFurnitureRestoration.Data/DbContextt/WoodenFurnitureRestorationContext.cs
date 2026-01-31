using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.DbContextt
{
    public class WoodenFurnitureRestorationContext : DbContext
    {
        public WoodenFurnitureRestorationContext(DbContextOptions<WoodenFurnitureRestorationContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTag> CategoryTags { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceTag> InvoiceTags { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderTag> OrderTags { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentTag> PaymentTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Restoration> Restorations { get; set; }
        public DbSet<RestorationService> RestorationServices { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingTag> ShippingTags { get; set; }
        public DbSet<ShippingProduct> ShippingProducts { get; set; }
        public DbSet<ShippingPayment> ShippingPayments { get; set; }
        public DbSet<ShippingInventory> ShippingInventories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierCategory> SupplierCategories { get; set; }
        public DbSet<SupplierMaterial> SupplierMaterials { get; set; }
        public DbSet<SupplierMaterialTag> SupplierMaterialTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Addresses");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(255);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Addresses)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Payments)
                    .WithOne(p => p.Address)
                    .HasForeignKey(p => p.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SupplierMaterials)
                    .WithOne(sm => sm.Address)
                    .HasForeignKey(sm => sm.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.Addresses)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Inventories)
                    .WithOne(i => i.Address)
                    .HasForeignKey(i => i.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Shippings)
                    .WithOne(s => s.Address)
                    .HasForeignKey(s => s.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.ToTable("BlogPosts");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BlogTitle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BlogContent)
                    .IsRequired();

                entity.Property(e => e.PublishedDate)
                    .IsRequired();

                entity.Property(e => e.BlogImage)
                    .HasMaxLength(255);

                entity.Property(e => e.BlogDescription)
                    .HasMaxLength(500);

                entity.Property(e => e.BlogAuthor)
                    .HasMaxLength(100);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.BlogPosts)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.BlogPosts)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.BlogPostTags)
                    .WithOne(bpt => bpt.BlogPost)
                    .HasForeignKey(bpt => bpt.BlogPostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BlogPostTag>(entity =>
            {
                entity.ToTable("BlogPostTags");
                entity.HasKey(e => new { e.BlogPostId, e.TagId });

                entity.HasOne(e => e.BlogPost)
                    .WithMany(bp => bp.BlogPostTags)
                    .HasForeignKey(e => e.BlogPostId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.BlogPostTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CategoryDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasMany(e => e.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Restorations)
                    .WithOne(r => r.Category)
                    .HasForeignKey(r => r.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.RestorationServices)
                    .WithOne(rs => rs.Category)
                    .HasForeignKey(rs => rs.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SupplierMaterials)
                    .WithOne(sm => sm.Category)
                    .HasForeignKey(sm => sm.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.BlogPosts)
                    .WithOne(bp => bp.Category)
                    .HasForeignKey(bp => bp.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.Categories)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CategoryTag>(entity =>
            {
                entity.ToTable("CategoryTags");
                entity.HasKey(e => new { e.CategoryId, e.TagId });

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.CategoryTags)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.CategoryTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerFirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerLastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerCity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerPostalCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CustomerImage)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Addresses)
                    .WithOne(a => a.Customer)
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Reviews)
                    .WithOne(r => r.Customer)
                    .HasForeignKey(r => r.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.BlogPosts)
                    .WithOne(bp => bp.Customer)
                    .HasForeignKey(bp => bp.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventories");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.QuantityInStock)
                    .IsRequired();

                entity.Property(e => e.LastUpdate)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.TotalAmount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.InventoryDate)
                    .IsRequired();

                entity.HasOne(e => e.Address)
                    .WithMany(a => a.Inventories)
                    .HasForeignKey(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SupplierMaterial)
                    .WithMany(sm => sm.Inventories)
                    .HasForeignKey(e => e.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(i => i.ShippingInventories)
                    .WithOne(si => si.Inventory)
                    .HasForeignKey(si => si.InventoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoices");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.InvoiceDate)
                    .IsRequired();

                entity.Property(e => e.TotalAmount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.NetAmount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.Invoices)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Payment)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(e => e.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.Invoices)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.InvoiceTags)
                    .WithOne(it => it.Invoice)
                    .HasForeignKey(it => it.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.SupplierMaterial)
    .WithMany(sm => sm.Invoices)
    .HasForeignKey(e => e.SupplierMaterialId)
    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<InvoiceTag>(entity =>
            {
                entity.ToTable("InvoiceTags");
                entity.HasKey(e => new { e.InvoiceId, e.TagId });

                entity.HasOne(e => e.Invoice)
                    .WithMany(i => i.InvoiceTags)
                    .HasForeignKey(e => e.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.InvoiceTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.OrderDate)
                    .IsRequired();

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerId)
                    .IsRequired();

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.Orders)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Invoices)
                    .WithOne(i => i.Order)
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Payments)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.OrderTags)
                    .WithOne(ot => ot.Order)
                    .HasForeignKey(ot => ot.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Shipping)
    .WithMany(s => s.Orders)
    .HasForeignKey(e => e.ShippingId)
    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SupplierMaterial)
                    .WithMany(sm => sm.Orders)
                    .HasForeignKey(e => e.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetails");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.OrderId)
                    .IsRequired();

                entity.Property(e => e.RestorationId)
                    .IsRequired();

                entity.Property(e => e.ProductId)
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired()
                    .HasColumnType("int");

                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Restoration)
                    .WithMany(r => r.OrderDetails)
                    .HasForeignKey(e => e.RestorationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderTag>(entity =>
            {
                entity.ToTable("OrderTags");
                entity.HasKey(e => new { e.OrderId, e.TagId });

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderTags)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.OrderTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PaymentDate)
                    .IsRequired();

                entity.Property(e => e.PaymentAmount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AddressId)
                    .IsRequired();

                entity.Property(e => e.OrderId)
                    .IsRequired();

                entity.Property(e => e.ShippingId)
                    .IsRequired(false);

                entity.Property(e => e.SupplierId)
                    .IsRequired();

                entity.Property(e => e.SupplierMaterialId)
                    .IsRequired();

                entity.HasOne(e => e.Address)
                    .WithMany(a => a.Payments)
                    .HasForeignKey(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.Payments)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.Payments)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Invoices)
                    .WithOne(i => i.Payment)
                    .HasForeignKey(i => i.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.ShippingPayments)
                    .WithOne(sp => sp.Payment)
                    .HasForeignKey(sp => sp.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.PaymentTags)
                    .WithOne(pt => pt.Payment)
                    .HasForeignKey(pt => pt.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.SupplierMaterial)
    .WithMany(sm => sm.Payments)
    .HasForeignKey(e => e.SupplierMaterialId)
    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Shipping)
                    .WithMany(s => s.Payments)
                    .HasForeignKey(e => e.ShippingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PaymentTag>(entity =>
            {
                entity.ToTable("PaymentTags");
                entity.HasKey(e => new { e.PaymentId, e.TagId });

                entity.HasOne(e => e.Payment)
                    .WithMany(p => p.PaymentTags)
                    .HasForeignKey(e => e.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.PaymentTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.Property(e => e.SupplierId)
                    .IsRequired();

                entity.Property(e => e.SupplierMaterialId)
                    .IsRequired();

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supplier)
                    .WithMany(s => s.Products)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.SupplierMaterial)
                    .WithMany(sm => sm.Products)
                    .HasForeignKey(p => p.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.Inventories)
                    .WithOne(i => i.Product)
                    .HasForeignKey(i => i.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.OrderDetails)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.Reviews)
                    .WithOne(r => r.Product)
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.ShippingProducts)
                    .WithOne(s => s.Product)
                    .HasForeignKey(s => s.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.ProductTags)
                    .WithOne(pt => pt.Product)
                    .HasForeignKey(pt => pt.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.ToTable("ProductTags");
                entity.HasKey(e => new { e.ProductId, e.TagId });

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.ProductTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Restoration>(entity =>
            {
                entity.ToTable("Restorations");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.RestorationName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RestorationDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RestorationPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.RestorationImage)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RestorationStatus)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RestorationDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.RestorationEndDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Restorations)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OrderDetails)
                    .WithOne(od => od.Restoration)
                    .HasForeignKey(od => od.RestorationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.RestorationServices)
                    .WithOne(rs => rs.Restoration)
                    .HasForeignKey(rs => rs.RestorationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RestorationService>(entity =>
            {
                entity.ToTable("RestorationServices");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.RestorationServiceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RestorationServiceDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RestorationServicePrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.RestorationServiceImage)
                    .HasMaxLength(250);

                entity.Property(e => e.RestorationServiceStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RestorationServiceDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.Property(e => e.RestorationId)
                    .IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.RestorationServices)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Restoration)
                    .WithMany(r => r.RestorationServices)
                    .HasForeignKey(e => e.RestorationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerId)
                    .IsRequired();

                entity.Property(e => e.ProductId)
                    .IsRequired();

                entity.Property(e => e.ReviewDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ReviewDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasColumnType("int");

                entity.Property(e => e.ReviewStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Supplier)
    .WithMany(s => s.Reviews)
    .HasForeignKey(e => e.SupplierId)
    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SupplierMaterial)
                    .WithMany(sm => sm.Reviews)
                    .HasForeignKey(e => e.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Restoration)
                    .WithMany(r => r.Reviews)
                    .HasForeignKey(e => e.RestorationId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("Shippings");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ShippingDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.ShippingType)
                    .IsRequired();

                entity.Property(e => e.ShippingCost)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ShippingStatus)
                    .IsRequired();

                entity.Property(e => e.OrderId)
                    .IsRequired();

                entity.Property(e => e.AddressId)
                    .IsRequired();

                entity.Property(e => e.SupplierId)
                    .IsRequired();

                entity.Property(e => e.SupplierMaterialId)
                    .IsRequired();

                entity.HasOne(e => e.Address)
                    .WithMany(a => a.Shippings)
                    .HasForeignKey(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.Shippings)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.ShippingTags)
                    .WithOne(st => st.Shipping)
                    .HasForeignKey(st => st.ShippingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ShippingProducts)
                    .WithOne(sp => sp.Shipping)
                    .HasForeignKey(sp => sp.ShippingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.ShippingPayments)
                    .WithOne(sp => sp.Shipping)
                    .HasForeignKey(sp => sp.ShippingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.ShippingInventories)
                    .WithOne(si => si.Shipping)
                    .HasForeignKey(si => si.ShippingId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<ShippingTag>(entity =>
            {
                entity.ToTable("ShippingTags");
                entity.HasKey(e => new { e.ShippingId, e.TagId });

                entity.HasOne(e => e.Shipping)
                    .WithMany(s => s.ShippingTags)
                    .HasForeignKey(e => e.ShippingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.ShippingTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ShippingProduct>(entity =>
            {
                entity.HasKey(e => new { e.ShippingId, e.ProductId });

                entity.HasOne(e => e.Shipping)
                    .WithMany(s => s.ShippingProducts)
                    .HasForeignKey(e => e.ShippingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ShippingProducts)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ShippingPayment>(entity =>
            {
                entity.HasKey(sp => new { sp.ShippingId, sp.PaymentId });

                entity.HasOne(sp => sp.Shipping)
                    .WithMany(s => s.ShippingPayments)
                    .HasForeignKey(sp => sp.ShippingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sp => sp.Payment)
                    .WithMany(p => p.ShippingPayments)
                    .HasForeignKey(sp => sp.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ShippingInventory>(entity =>
            {
                entity.HasKey(si => new { si.ShippingId, si.InventoryId });

                entity.HasOne(si => si.Shipping)
                      .WithMany(s => s.ShippingInventories)
                      .HasForeignKey(si => si.ShippingId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(si => si.Inventory)
                      .WithMany(i => i.ShippingInventories)
                      .HasForeignKey(si => si.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SupplierAddress)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SupplierEmail)
                    .HasMaxLength(100);

                entity.Property(e => e.SupplierPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.HasMany(e => e.Addresses)
                    .WithOne(a => a.Supplier)
                    .HasForeignKey(a => a.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Products)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SupplierMaterials)
                    .WithOne(sm => sm.Supplier)
                    .HasForeignKey(sm => sm.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Shippings)
                    .WithOne(s => s.Supplier)
                    .HasForeignKey(s => s.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Payments)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Orders)
                    .WithOne(o => o.Supplier)
                    .HasForeignKey(o => o.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Invoices)
                    .WithOne(i => i.Supplier)
                    .HasForeignKey(i => i.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Categories)
                    .WithOne(c => c.Supplier)
                    .HasForeignKey(c => c.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SupplierCategory>(entity =>
            {
                entity.HasKey(sc => new { sc.SupplierId, sc.CategoryId });

                entity.HasOne(sc => sc.Supplier)
                    .WithMany(s => s.SupplierCategories)
                    .HasForeignKey(sc => sc.SupplierId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(sc => sc.Category)
                    .WithMany(c => c.SupplierCategories)
                    .HasForeignKey(sc => sc.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SupplierMaterial>(entity =>
            {
                entity.ToTable("SupplierMaterials");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.MaterialQuantity)
                    .IsRequired()
                    .HasDefaultValue(1);

                entity.Property(e => e.MaterialStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.MaterialPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.MaterialDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MaterialCategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialColor)
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialSize)
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialBrand)
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialModel)
                    .HasMaxLength(50);

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.SupplierMaterials)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Address)
                    .WithMany(a => a.SupplierMaterials)
                    .HasForeignKey(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.SupplierMaterials)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Products)
                    .WithOne(p => p.SupplierMaterial)
                    .HasForeignKey(p => p.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Inventories)
                    .WithOne(i => i.SupplierMaterial)
                    .HasForeignKey(i => i.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(sm => sm.SupplierMaterialTags)
                    .WithOne(smt => smt.SupplierMaterial)
                    .HasForeignKey(smt => smt.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Orders)
    .WithOne(o => o.SupplierMaterial)
    .HasForeignKey(o => o.SupplierMaterialId)
    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Payments)
                    .WithOne(p => p.SupplierMaterial)
                    .HasForeignKey(p => p.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Invoices)
                    .WithOne(i => i.SupplierMaterial)
                    .HasForeignKey(i => i.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Shippings)
                    .WithOne(s => s.SupplierMaterial)
                    .HasForeignKey(s => s.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<SupplierMaterialTag>(entity =>
            {
                entity.HasKey(smt => new { smt.SupplierMaterialId, smt.TagId });

                entity.HasOne(smt => smt.SupplierMaterial)
                    .WithMany(sm => sm.SupplierMaterialTags)
                    .HasForeignKey(smt => smt.SupplierMaterialId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(smt => smt.Tag)
                    .WithMany(t => t.SupplierMaterialTags)
                    .HasForeignKey(smt => smt.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tags");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(e => e.BlogPostTags)
                    .WithOne(bpt => bpt.Tag)
                    .HasForeignKey(bpt => bpt.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.CategoryTags)
                    .WithOne(ct => ct.Tag)
                    .HasForeignKey(ct => ct.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.InvoiceTags)
                    .WithOne(it => it.Tag)
                    .HasForeignKey(it => it.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.OrderTags)
                    .WithOne(ot => ot.Tag)
                    .HasForeignKey(ot => ot.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.PaymentTags)
                    .WithOne(pt => pt.Tag)
                    .HasForeignKey(pt => pt.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ProductTags)
                    .WithOne(pt => pt.Tag)
                    .HasForeignKey(pt => pt.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ShippingTags)
                    .WithOne(st => st.Tag)
                    .HasForeignKey(st => st.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.SupplierMaterialTags)
                    .WithOne(smt => smt.Tag)
                    .HasForeignKey(smt => smt.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}