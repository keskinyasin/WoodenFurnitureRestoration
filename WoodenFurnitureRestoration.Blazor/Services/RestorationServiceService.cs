using WoodenFurnitureRestoration.Shared.DTOs.RestorationService;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class RestorationServiceService(HttpClient httpClient, ILogger<RestorationServiceService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/restorationservices";

    public async Task<List<RestorationServiceDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all restoration services from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<RestorationServiceDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching restoration services");
            return [];
        }
    }

    public async Task<RestorationServiceDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<RestorationServiceDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching restoration service {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateRestorationServiceDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating restoration service");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateRestorationServiceDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating restoration service {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting restoration service {Id}", id);
            return false;
        }
    }
}