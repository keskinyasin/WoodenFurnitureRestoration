using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WoodenFurnitureRestorationContext _context;

        public UnitOfWork(WoodenFurnitureRestorationContext context)
        {
            _context = context;
            AddressRepository = new AddressRepository(_context);
            BlogPostRepository = new BlogPostRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            CustomerRepository = new CustomerRepository(_context);
            InventoryRepository = new InventoryRepository(_context);
            InvoiceRepository = new InvoiceRepository(_context);
            OrderRepository = new OrderRepository(_context);
            OrderDetailRepository = new OrderDetailRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            ProductRepository = new ProductRepository(_context);
            RestorationRepository = new RestorationRepository(_context);
            RestorationServiceRepository = new RestorationServiceRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            ShippingRepository = new ShippingRepository(_context);
            SupplierMaterialRepository = new SupplierMaterialRepository(_context);
            SupplierRepository = new SupplierRepository(_context);
            TagRepository = new TagRepository(_context);

            Addresses = new Repository<Address>(_context);
            BlogPosts = new Repository<BlogPost>(_context);
            Categories = new Repository<Category>(_context);
            Customers = new Repository<Customer>(_context);
            Inventories = new Repository<Inventory>(_context);
            Invoices = new Repository<Invoice>(_context);
            Orders = new Repository<Order>(_context);
            OrderDetails = new Repository<OrderDetail>(_context);
            Payments = new Repository<Payment>(_context);
            Products = new Repository<Product>(_context);
            Restorations = new Repository<Restoration>(_context);
            RestorationServices = new Repository<RestorationService>(_context);
            Reviews = new Repository<Review>(_context);
            Shippings = new Repository<Shipping>(_context);
            SupplierMaterials = new Repository<SupplierMaterial>(_context);
            Suppliers = new Repository<Supplier>(_context);
            Tags = new Repository<Tag>(_context);
        }

        public IAddressRepository AddressRepository { get; private set; }
        public IBlogPostRepository BlogPostRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public IInventoryRepository InventoryRepository { get; private set; }
        public IInvoiceRepository InvoiceRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IOrderDetailRepository OrderDetailRepository { get; private set; }
        public IPaymentRepository PaymentRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IRestorationRepository RestorationRepository { get; private set; }
        public IRestorationServiceRepository RestorationServiceRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public IShippingRepository ShippingRepository { get; private set; }
        public ISupplierMaterialRepository SupplierMaterialRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }

        public IRepository<Address> Addresses { get; private set; }
        public IRepository<BlogPost> BlogPosts { get; private set; }
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Customer> Customers { get; private set; }
        public IRepository<Inventory> Inventories { get; private set; }
        public IRepository<Invoice> Invoices { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderDetail> OrderDetails { get; private set; }
        public IRepository<Payment> Payments { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Restoration> Restorations { get; private set; }
        public IRepository<RestorationService> RestorationServices { get; private set; }
        public IRepository<Review> Reviews { get; private set; }
        public IRepository<Shipping> Shippings { get; private set; }
        public IRepository<SupplierMaterial> SupplierMaterials { get; private set; }
        public IRepository<Supplier> Suppliers { get; private set; }
        public IRepository<Tag> Tags { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
