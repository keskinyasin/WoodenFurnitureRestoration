using WoodenFurnitureRestoration.Shared.DTOs.Tag;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class TagService(HttpClient httpClient, ILogger<TagService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/tags";

    public async Task<List<TagDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all tags from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<TagDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching tags");
            return [];
        }
    }

    public async Task<TagDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<TagDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching tag {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateTagDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating tag");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateTagDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating tag {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting tag {Id}", id);
            return false;
        }
    }
}