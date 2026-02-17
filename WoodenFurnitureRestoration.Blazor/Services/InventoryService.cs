using WoodenFurnitureRestoration.Shared.DTOs.Inventory;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class InventoryService(HttpClient httpClient, ILogger<InventoryService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/inventories";

    public async Task<List<InventoryDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all inventories from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<InventoryDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching inventories");
            return [];
        }
    }

    public async Task<InventoryDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<InventoryDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching inventory {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateInventoryDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating inventory");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateInventoryDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating inventory {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting inventory {Id}", id);
            return false;
        }
    }
}