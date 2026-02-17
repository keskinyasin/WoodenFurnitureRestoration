using WoodenFurnitureRestoration.Shared.DTOs.Restoration;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class RestorationService(HttpClient httpClient, ILogger<RestorationService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/restorations";

    public async Task<List<RestorationDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all restorations from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<RestorationDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching restorations");
            return [];
        }
    }

    public async Task<RestorationDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<RestorationDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching restoration {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateRestorationDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating restoration");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateRestorationDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating restoration {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting restoration {Id}", id);
            return false;
        }
    }
}