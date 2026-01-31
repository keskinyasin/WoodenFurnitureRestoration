using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class InvoiceService(IUnitOfWork unitOfWork, IMapper mapper) : IInvoiceService
{
    #region CRUD Operations

    public async Task<List<Invoice>> GetAllAsync()
    {
        return await unitOfWork.InvoiceRepository.GetAllAsync(i => !i.Deleted);
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir fatura ID'si gereklidir.", nameof(id));

        return await unitOfWork.InvoiceRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        ValidateInvoice(invoice);

        try
        {
            invoice.CreatedDate = DateTime.Now;
            invoice.UpdatedDate = DateTime.Now;
            invoice.Deleted = false;
            invoice.NetAmount = CalculateNetAmount(invoice.TotalAmount, invoice.Discount);
            await unitOfWork.InvoiceRepository.AddAsync(invoice);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Fatura kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        ValidateInvoice(invoice);

        var existing = await unitOfWork.InvoiceRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(invoice, existing);
            existing.UpdatedDate = DateTime.Now;
            existing.NetAmount = CalculateNetAmount(existing.TotalAmount, existing.Discount);
            await unitOfWork.InvoiceRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Fatura güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var invoice = await unitOfWork.InvoiceRepository.FindAsync(id);
        if (invoice is null) return false;

        try
        {
            // Soft Delete
            invoice.Deleted = true;
            invoice.UpdatedDate = DateTime.Now;
            await unitOfWork.InvoiceRepository.UpdateAsync(invoice);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Fatura silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Invoice>> GetInvoicesByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        return await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            i.OrderId == orderId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            i.SupplierId == supplierId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesByPaymentAsync(int paymentId)
    {
        if (paymentId <= 0)
            throw new ArgumentException("Geçerli bir ödeme ID'si gereklidir.", nameof(paymentId));

        return await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            i.PaymentId == paymentId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesByShippingAsync(int shippingId)
    {
        if (shippingId <= 0)
            throw new ArgumentException("Geçerli bir kargo ID'si gereklidir.", nameof(shippingId));

        return await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            i.ShippingId == shippingId && !i.Deleted);
    }

    public async Task<List<Invoice>> GetInvoicesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            i.InvoiceDate >= startDate && i.InvoiceDate <= endDate && !i.Deleted);
    }

    public async Task<Invoice?> GetInvoiceByOrderIdAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        var invoices = await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            i.OrderId == orderId && !i.Deleted);

        return invoices.FirstOrDefault();
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var invoices = await unitOfWork.InvoiceRepository.GetAllAsync(i =>
            !i.Deleted &&
            (!startDate.HasValue || i.InvoiceDate >= startDate.Value) &&
            (!endDate.HasValue || i.InvoiceDate <= endDate.Value));

        return invoices.Sum(i => i.NetAmount);
    }

    public async Task<bool> ApplyDiscountAsync(int invoiceId, decimal discountAmount)
    {
        if (discountAmount < 0)
            throw new ArgumentException("İndirim miktarı 0'dan küçük olamaz.", nameof(discountAmount));

        var invoice = await unitOfWork.InvoiceRepository.FindAsync(invoiceId);
        if (invoice is null) return false;

        if (discountAmount > invoice.TotalAmount)
            throw new InvalidOperationException("İndirim miktarı toplam tutardan büyük olamaz.");

        try
        {
            invoice.Discount = discountAmount;
            invoice.NetAmount = CalculateNetAmount(invoice.TotalAmount, discountAmount);
            invoice.UpdatedDate = DateTime.Now;
            await unitOfWork.InvoiceRepository.UpdateAsync(invoice);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("İndirim uygulanırken bir hata oluştu.", ex);
        }
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
        return await unitOfWork.InvoiceRepository.GetAllAsync(i =>
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

    #endregion

    #region Private Methods

    private static decimal CalculateNetAmount(decimal totalAmount, decimal discount)
    {
        return totalAmount - discount;
    }

    #endregion

    #region Validation

    private static void ValidateInvoice(Invoice invoice)
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

    #endregion
}