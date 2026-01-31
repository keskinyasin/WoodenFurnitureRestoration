using System;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        // ✅ SADECE TİPED REPOSITORIES (custom metodları olan)
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
        IRestorationRepository RestorationRepository { get; }
        IRestorationServiceRepository RestorationServiceRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IShippingRepository ShippingRepository { get; }
        ISupplierMaterialRepository SupplierMaterialRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ITagRepository TagRepository { get; }

        // ✅ SAVE METHOD
        Task<int> SaveChangesAsync();
    }
}