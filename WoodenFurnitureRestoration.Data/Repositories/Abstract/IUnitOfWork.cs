using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }
        IBlogPostRepository BlogPostRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        IRepository<Address> Addresses { get; }
        IRepository<BlogPost> BlogPosts { get; }
        IRepository<Category> Categories { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Inventory> Inventories { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderDetail> OrderDetails { get; }
        IRepository<Payment> Payments { get; }
        IRepository<Product> Products { get; }
        IRepository<Restoration> Restorations { get; }
        IRepository<RestorationService> RestorationServices { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Shipping> Shippings { get; }
        IRepository<SupplierMaterial> SupplierMaterials { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<Tag> Tags { get; }
        IRestorationRepository RestorationRepository { get; }
        IRestorationServiceRepository RestorationServiceRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IShippingRepository ShippingRepository { get; }
        ISupplierMaterialRepository SupplierMaterialRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ITagRepository TagRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
