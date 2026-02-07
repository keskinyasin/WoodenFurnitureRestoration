using WoodenFurnitureRestoration.Shared.DTOs.Customer;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class CustomerService(HttpClient httpClient, ILogger<CustomerService> logger)
{
    private const string ApiUrl = "https://localhost:7001/api/customers";

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all customers from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<CustomerDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching customers");
            return [];
        }
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CustomerDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching customer {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateCustomerDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating customer");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateCustomerDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating customer {Id}", id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"{ApiUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error deleting customer {Id}", id);
            return false;
        }
    }
}