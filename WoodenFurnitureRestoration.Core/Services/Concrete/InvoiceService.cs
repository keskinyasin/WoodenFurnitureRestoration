using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Invoice>(unitOfWork), IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected override IRepository<Invoice> Repository => _unitOfWork.InvoiceRepository;

    protected override void ValidateEntity(Invoice invoice)
    {
        if (invoice.InvoiceDate == default)
            throw new ArgumentException("Fatura tarihi gereklidir.", nameof(invoice));
        if (invoice.TotalAmount <= 0)
            throw new ArgumentException("Toplam tutar 0'dan büyük olmalıdır.", nameof(invoice));
        if (invoice.Discount < 0)
            throw new ArgumentException("İndirim 0'dan küçük olamaz.", nameof(invoice));
        if (invoice.Discount > invoice.TotalAmount)
            throw new ArgumentException("İndirim toplam tutardan büyük olamaz.", nameof(invoice));
        if (invoice.OrderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş seçilmelidir.", nameof(invoice));
        if (invoice.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(invoice));
        if (invoice.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(invoice));
    }

    private static decimal CalculateNetAmount(decimal totalAmount, decimal discount)
    {
        return totalAmount - discount;
    }

    public async Task<List<Invoice>> GetInvoicesByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        return await Repository.GetAllAsync(i => i.OrderId == orderId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));
        return await Repository.GetAllAsync(i => i.SupplierId == supplierId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesByPaymentAsync(int paymentId)
    {
        if (paymentId <= 0)
            throw new ArgumentException("Geçerli bir ödeme ID'si gereklidir.", nameof(paymentId));
        return await Repository.GetAllAsync(i => i.PaymentId == paymentId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesByShippingAsync(int shippingId)
    {
        if (shippingId <= 0)
            throw new ArgumentException("Geçerli bir kargo ID'si gereklidir.", nameof(shippingId));
        return await Repository.GetAllAsync(i => i.ShippingId == shippingId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));
        return await Repository.GetAllAsync(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate && !i.Deleted);
    }

    public async Task<Invoice?> GetInvoiceByOrderIdAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        var invoices = await Repository.GetAllAsync(i => i.OrderId == orderId && !i.Deleted);
        return invoices.FirstOrDefault();
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var invoices = await Repository.GetAllAsync(i =>
            !i.Deleted &&
            (!startDate.HasValue || i.InvoiceDate >= startDate.Value) &&
            (!endDate.HasValue || i.InvoiceDate <= endDate.Value));
        return invoices.Sum(i => i.NetAmount);
    }

    public async Task<bool> ApplyDiscountAsync(int invoiceId, decimal discountAmount)
    {
        if (discountAmount < 0)
            throw new ArgumentException("İndirim miktarı 0'dan küçük olamaz.", nameof(discountAmount));
        var invoice = await Repository.FindAsync(invoiceId);
        if (invoice is null) return false;
        if (discountAmount > invoice.TotalAmount)
            throw new InvalidOperationException("İndirim miktarı toplam tutardan büyük olamaz.");

        invoice.Discount = discountAmount;
        invoice.NetAmount = CalculateNetAmount(invoice.TotalAmount, discountAmount);
        invoice.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(invoice);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<Invoice>> GetInvoicesByFiltersAsync(
        int? orderId = null,
        int? supplierId = null,
        int? paymentId = null,
        int? shippingId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null)
    {
        return await Repository.GetAllAsync(i =>
            !i.Deleted &&
            (!orderId.HasValue || i.OrderId == orderId.Value) &&
            (!supplierId.HasValue || i.SupplierId == supplierId.Value) &&
            (!paymentId.HasValue || i.PaymentId == paymentId.Value) &&
            (!shippingId.HasValue || i.ShippingId == shippingId.Value) &&
            (!startDate.HasValue || i.InvoiceDate >= startDate.Value) &&
            (!endDate.HasValue || i.InvoiceDate <= endDate.Value) &&
            (!minAmount.HasValue || i.NetAmount >= minAmount.Value) &&
            (!maxAmount.HasValue || i.NetAmount <= maxAmount.Value));
    }
}