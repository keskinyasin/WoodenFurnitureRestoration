using WoodenFurnitureRestoration.Shared.DTOs.Category;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class CategoryService(HttpClient httpClient, ILogger<CategoryService> logger)
{
    private const string ApiUrl = "https://localhost:7265/api/categories";

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all categories from API");
            var response = await httpClient.GetAsync(ApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("⚠️ API returned {StatusCode}", response.StatusCode);
                return [];
            }

            return await response.Content.ReadFromJsonAsync<List<CategoryDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching categories");
            return [];
        }
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CategoryDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching category {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateCategoryDto dto)
    {
        try
        {
            logger.LogInformation("📤 Creating category: {Name}", dto.Name);
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                logger.LogWarning("⚠️ Create failed: {StatusCode} - {Error}", response.StatusCode, error);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating category");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        try
        {
            logger.LogInformation("📤 Updating category {Id}: {Name}", id, dto.Name);
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                logger.LogWarning("⚠️ Update failed: {StatusCode} - {Error}", response.StatusCode, error);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating category {Id}", id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("🗑️ Deleting category {Id}", id);
            var response = await httpClient.DeleteAsync($"{ApiUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error deleting category {Id}", id);
            return false;
        }
    }
}