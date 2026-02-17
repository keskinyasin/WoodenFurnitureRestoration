using WoodenFurnitureRestoration.Shared.DTOs.Supplier;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class SupplierService(HttpClient httpClient, ILogger<SupplierService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/suppliers";

    public async Task<List<SupplierDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all suppliers from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<SupplierDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching suppliers");
            return [];
        }
    }

    public async Task<SupplierDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<SupplierDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching supplier {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateSupplierDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating supplier");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateSupplierDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating supplier {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting supplier {Id}", id);
            return false;
        }
    }
}