using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Customer>(unitOfWork), ICustomerService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Customer> Repository => unitOfWork.CustomerRepository;

    protected override void ValidateEntity(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.CustomerFirstName))
            throw new ArgumentException("Müşteri adı gereklidir.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerLastName))
            throw new ArgumentException("Müşteri soyadı gereklidir.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerEmail))
            throw new ArgumentException("E-posta adresi gereklidir.", nameof(customer));
        if (!IsValidEmail(customer.CustomerEmail))
            throw new ArgumentException("Geçersiz e-posta adresi.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerPassword))
            throw new ArgumentException("Şifre gereklidir.", nameof(customer));
        if (customer.CustomerPassword.Length < 6)
            throw new ArgumentException("Şifre en az 6 karakter olmalıdır.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerPhone))
            throw new ArgumentException("Telefon numarası gereklidir.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerCity))
            throw new ArgumentException("Şehir gereklidir.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerCountry))
            throw new ArgumentException("Ülke gereklidir.", nameof(customer));
        if (string.IsNullOrWhiteSpace(customer.CustomerPostalCode))
            throw new ArgumentException("Posta kodu gereklidir.", nameof(customer));
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi gereklidir.", nameof(email));

        var customers = await Repository.GetAllAsync(c =>
            c.CustomerEmail == email && !c.Deleted);

        return customers.FirstOrDefault();
    }

    public async Task<List<Customer>> GetCustomersByCityAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Şehir adı gereklidir.", nameof(city));

        return await Repository.GetAllAsync(c =>
            c.CustomerCity.Contains(city) && !c.Deleted);
    }

    public async Task<List<Customer>> GetCustomersByCountryAsync(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Ülke adı gereklidir.", nameof(country));

        return await Repository.GetAllAsync(c =>
            c.CustomerCountry.Contains(country) && !c.Deleted);
    }

    public async Task<List<Customer>> GetCustomersByAddressAndRestorationAsync(
        string city,
        string district,
        string country,
        int restorationId)
    {
        if (restorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(restorationId));

        // Eğer özel bir repository metodu varsa kullan, yoksa uygun şekilde düzenle
        return await unitOfWork.CustomerRepository.GetCustomersByAddressAndRestorationAsync(
            city, district, country, restorationId);
    }

    public async Task<List<Customer>> GetCustomersByFiltersAsync(
        string? firstName = null,
        string? lastName = null,
        string? email = null,
        string? city = null,
        string? country = null)
    {
        return await Repository.GetAllAsync(c =>
            !c.Deleted &&
            (string.IsNullOrEmpty(firstName) || c.CustomerFirstName.Contains(firstName)) &&
            (string.IsNullOrEmpty(lastName) || c.CustomerLastName.Contains(lastName)) &&
            (string.IsNullOrEmpty(email) || c.CustomerEmail.Contains(email)) &&
            (string.IsNullOrEmpty(city) || c.CustomerCity.Contains(city)) &&
            (string.IsNullOrEmpty(country) || c.CustomerCountry.Contains(country)));
    }

    public async Task<bool> ChangePasswordAsync(int customerId, string currentPassword, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(currentPassword))
            throw new ArgumentException("Mevcut şifre gereklidir.", nameof(currentPassword));
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("Yeni şifre gereklidir.", nameof(newPassword));
        if (newPassword.Length < 6)
            throw new ArgumentException("Yeni şifre en az 6 karakter olmalıdır.", nameof(newPassword));

        var customer = await Repository.FindAsync(customerId);
        if (customer is null) return false;

        if (customer.CustomerPassword != currentPassword)
            throw new InvalidOperationException("Mevcut şifre hatalı.");

        customer.CustomerPassword = newPassword;
        customer.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(customer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}