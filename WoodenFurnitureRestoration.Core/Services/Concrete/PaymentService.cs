using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class PaymentService(IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
{
    // Ödeme Durumları
    private static class PaymentStatuses
    {
        public const string Pending = "Beklemede";
        public const string Processing = "İşleniyor";
        public const string Completed = "Tamamlandı";
        public const string Failed = "Başarısız";
        public const string Refunded = "İade Edildi";
        public const string Cancelled = "İptal Edildi";
    }

    // Ödeme Yöntemleri
    private static class PaymentMethods
    {
        public const string CreditCard = "Kredi Kartı";
        public const string DebitCard = "Banka Kartı";
        public const string BankTransfer = "Havale/EFT";
        public const string Cash = "Nakit";
        public const string PayPal = "PayPal";
    }

    #region CRUD Operations

    public async Task<List<Payment>> GetAllAsync()
    {
        return await unitOfWork.PaymentRepository.GetAllAsync(p => !p.Deleted);
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir ödeme ID'si gereklidir.", nameof(id));

        return await unitOfWork.PaymentRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);
        ValidatePayment(payment);

        try
        {
            payment.CreatedDate = DateTime.Now;
            payment.UpdatedDate = DateTime.Now;
            payment.PaymentDate = DateTime.Now;
            payment.Deleted = false;
            payment.PaymentStatus = PaymentStatuses.Pending;
            await unitOfWork.PaymentRepository.AddAsync(payment);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ödeme kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);
        ValidatePayment(payment);

        var existing = await unitOfWork.PaymentRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(payment, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.PaymentRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ödeme güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await unitOfWork.PaymentRepository.FindAsync(id);
        if (payment is null) return false;

        try
        {
            // Soft Delete
            payment.Deleted = true;
            payment.UpdatedDate = DateTime.Now;
            await unitOfWork.PaymentRepository.UpdateAsync(payment);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ödeme silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Payment>> GetPaymentsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.OrderId == orderId && !p.Deleted);
    }

    public async Task<List<Payment>> GetPaymentsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.SupplierId == supplierId && !p.Deleted);
    }

    public async Task<List<Payment>> GetPaymentsByAddressAsync(int addressId)
    {
        if (addressId <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(addressId));

        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.AddressId == addressId && !p.Deleted);
    }

    public async Task<List<Payment>> GetPaymentsByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Ödeme durumu gereklidir.", nameof(status));

        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.PaymentStatus == status && !p.Deleted);
    }

    public async Task<List<Payment>> GetPaymentsByMethodAsync(string method)
    {
        if (string.IsNullOrWhiteSpace(method))
            throw new ArgumentException("Ödeme yöntemi gereklidir.", nameof(method));

        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.PaymentMethod == method && !p.Deleted);
    }

    public async Task<List<Payment>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.PaymentDate >= startDate && p.PaymentDate <= endDate && !p.Deleted);
    }

    public async Task<bool> UpdatePaymentStatusAsync(int paymentId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Ödeme durumu gereklidir.", nameof(status));

        var payment = await unitOfWork.PaymentRepository.FindAsync(paymentId);
        if (payment is null) return false;

        try
        {
            payment.PaymentStatus = status;
            payment.UpdatedDate = DateTime.Now;
            await unitOfWork.PaymentRepository.UpdateAsync(payment);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ödeme durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<decimal> GetTotalPaymentsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        var payments = await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.OrderId == orderId &&
            p.PaymentStatus == PaymentStatuses.Completed &&
            !p.Deleted);

        return payments.Sum(p => p.PaymentAmount);
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var payments = await unitOfWork.PaymentRepository.GetAllAsync(p =>
            !p.Deleted &&
            p.PaymentStatus == PaymentStatuses.Completed &&
            (!startDate.HasValue || p.PaymentDate >= startDate.Value) &&
            (!endDate.HasValue || p.PaymentDate <= endDate.Value));

        return payments.Sum(p => p.PaymentAmount);
    }

    public async Task<List<Payment>> GetPendingPaymentsAsync()
    {
        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.PaymentStatus == PaymentStatuses.Pending && !p.Deleted);
    }

    public async Task<List<Payment>> GetCompletedPaymentsAsync()
    {
        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            p.PaymentStatus == PaymentStatuses.Completed && !p.Deleted);
    }

    public async Task<List<Payment>> GetPaymentsByFiltersAsync(
        int? orderId = null,
        int? supplierId = null,
        int? addressId = null,
        int? shippingId = null,
        string? status = null,
        string? method = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null)
    {
        return await unitOfWork.PaymentRepository.GetAllAsync(p =>
            !p.Deleted &&
            (!orderId.HasValue || p.OrderId == orderId.Value) &&
            (!supplierId.HasValue || p.SupplierId == supplierId.Value) &&
            (!addressId.HasValue || p.AddressId == addressId.Value) &&
            (!shippingId.HasValue || p.ShippingId == shippingId.Value) &&
            (string.IsNullOrEmpty(status) || p.PaymentStatus == status) &&
            (string.IsNullOrEmpty(method) || p.PaymentMethod == method) &&
            (!startDate.HasValue || p.PaymentDate >= startDate.Value) &&
            (!endDate.HasValue || p.PaymentDate <= endDate.Value) &&
            (!minAmount.HasValue || p.PaymentAmount >= minAmount.Value) &&
            (!maxAmount.HasValue || p.PaymentAmount <= maxAmount.Value));
    }

    #endregion

    #region Validation

    private static void ValidatePayment(Payment payment)
    {
        if (payment.PaymentAmount <= 0)
            throw new ArgumentException("Ödeme miktarı 0'dan büyük olmalıdır.", nameof(payment));

        if (string.IsNullOrWhiteSpace(payment.PaymentMethod))
            throw new ArgumentException("Ödeme yöntemi gereklidir.", nameof(payment));

        if (payment.OrderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş seçilmelidir.", nameof(payment));

        if (payment.AddressId <= 0)
            throw new ArgumentException("Geçerli bir adres seçilmelidir.", nameof(payment));

        if (payment.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(payment));

        if (payment.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(payment));
    }

    #endregion
}