using WoodenFurnitureRestoration.Shared.DTOs.Shipping;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class ShippingService(HttpClient httpClient, ILogger<ShippingService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/shippings";

    public async Task<List<ShippingDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all shippings from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<ShippingDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching shippings");
            return [];
        }
    }

    public async Task<ShippingDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ShippingDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching shipping {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateShippingDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating shipping");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateShippingDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating shipping {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting shipping {Id}", id);
            return false;
        }
    }
}