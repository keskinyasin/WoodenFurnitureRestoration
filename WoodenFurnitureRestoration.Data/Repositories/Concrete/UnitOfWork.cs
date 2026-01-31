using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WoodenFurnitureRestorationContext _context;

        public UnitOfWork(WoodenFurnitureRestorationContext context)
        {
            _context = context;

            // ✅ SADECE TİPED REPOSITORIES
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
        }

        // ✅ TİPED REPOSITORIES (Interface'den)
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

        // ✅ SAVE METHOD
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // ✅ DISPOSE
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}