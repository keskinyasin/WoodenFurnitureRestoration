using WoodenFurnitureRestoration.Shared.DTOs.Review;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class ReviewService(HttpClient httpClient, ILogger<ReviewService> logger)
{
    private const string ApiUrl = "https://localhost:7001/api/reviews";

    public async Task<List<ReviewDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all reviews from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<ReviewDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching reviews");
            return [];
        }
    }

    public async Task<ReviewDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ReviewDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching review {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateReviewDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating review");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateReviewDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating review {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting review {Id}", id);
            return false;
        }
    }
}