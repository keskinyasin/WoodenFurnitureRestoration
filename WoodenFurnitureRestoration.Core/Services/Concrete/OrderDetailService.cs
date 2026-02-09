using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<OrderDetail>(unitOfWork), IOrderDetailService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<OrderDetail> Repository => unitOfWork.OrderDetailRepository;

    protected override void ValidateEntity(OrderDetail orderDetail)
    {
        if (orderDetail.OrderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş seçilmelidir.", nameof(orderDetail));
        if (orderDetail.ProductId <= 0)
            throw new ArgumentException("Geçerli bir ürün seçilmelidir.", nameof(orderDetail));
        if (orderDetail.RestorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon seçilmelidir.", nameof(orderDetail));
        if (orderDetail.Quantity <= 0)
            throw new ArgumentException("Miktar 0'dan büyük olmalıdır.", nameof(orderDetail));
        if (orderDetail.UnitPrice <= 0)
            throw new ArgumentException("Birim fiyat 0'dan büyük olmalıdır.", nameof(orderDetail));
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        return await Repository.GetAllAsync(od => od.OrderId == orderId && !od.Deleted);
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));
        return await Repository.GetAllAsync(od => od.ProductId == productId && !od.Deleted);
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByRestorationAsync(int restorationId)
    {
        if (restorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(restorationId));
        return await Repository.GetAllAsync(od => od.RestorationId == restorationId && !od.Deleted);
    }

    public async Task<decimal> GetOrderTotalAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        var orderDetails = await Repository.GetAllAsync(od => od.OrderId == orderId && !od.Deleted);
        return orderDetails.Sum(od => od.Quantity * od.UnitPrice);
    }

    public async Task<decimal> GetLineItemTotalAsync(int orderDetailId)
    {
        if (orderDetailId <= 0)
            throw new ArgumentException("Geçerli bir sipariş detayı ID'si gereklidir.", nameof(orderDetailId));
        var orderDetail = await Repository.FindAsync(orderDetailId);
        if (orderDetail is null)
            throw new InvalidOperationException("Sipariş detayı bulunamadı.");
        return orderDetail.Quantity * orderDetail.UnitPrice;
    }

    public async Task<bool> UpdateQuantityAsync(int orderDetailId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Miktar 0'dan büyük olmalıdır.", nameof(quantity));
        var orderDetail = await Repository.FindAsync(orderDetailId);
        if (orderDetail is null) return false;

        orderDetail.Quantity = quantity;
        orderDetail.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(orderDetail);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByFiltersAsync(
        int? orderId = null,
        int? productId = null,
        int? restorationId = null,
        int? minQuantity = null,
        int? maxQuantity = null,
        decimal? minUnitPrice = null,
        decimal? maxUnitPrice = null)
    {
        return await Repository.GetAllAsync(od =>
            !od.Deleted &&
            (!orderId.HasValue || od.OrderId == orderId.Value) &&
            (!productId.HasValue || od.ProductId == productId.Value) &&
            (!restorationId.HasValue || od.RestorationId == restorationId.Value) &&
            (!minQuantity.HasValue || od.Quantity >= minQuantity.Value) &&
            (!maxQuantity.HasValue || od.Quantity <= maxQuantity.Value) &&
            (!minUnitPrice.HasValue || od.UnitPrice >= minUnitPrice.Value) &&
            (!maxUnitPrice.HasValue || od.UnitPrice <= maxUnitPrice.Value));
    }
}