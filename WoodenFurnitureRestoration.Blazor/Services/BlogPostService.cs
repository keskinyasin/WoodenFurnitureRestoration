using WoodenFurnitureRestoration.Shared.DTOs.BlogPost;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class BlogPostService(HttpClient httpClient, ILogger<BlogPostService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/blogPosts";

    public async Task<List<BlogPostDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all blog posts from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<BlogPostDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching blog posts");
            return [];
        }
    }

    public async Task<BlogPostDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<BlogPostDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching blog post {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateBlogPostDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating blog post");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating blog post {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting blog post {Id}", id);
            return false;
        }
    }
}