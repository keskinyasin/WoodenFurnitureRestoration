using WoodenFurnitureRestoration.Shared.DTOs.Product;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class ProductService(HttpClient httpClient, ILogger<ProductService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/products";

    public async Task<List<ProductDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all products from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<ProductDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching products");
            return [];
        }
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching product {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateProductDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating product");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating product {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting product {Id}", id);
            return false;
        }
    }
}