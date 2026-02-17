using WoodenFurnitureRestoration.Shared.DTOs.SupplierMaterial;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class SupplierMaterialService(HttpClient httpClient, ILogger<SupplierMaterialService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/suppliermaterials";

    public async Task<List<SupplierMaterialDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all supplier materials from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<SupplierMaterialDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching supplier materials");
            return [];
        }
    }

    public async Task<SupplierMaterialDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<SupplierMaterialDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching supplier material {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateSupplierMaterialDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating supplier material");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateSupplierMaterialDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating supplier material {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting supplier material {Id}", id);
            return false;
        }
    }
}