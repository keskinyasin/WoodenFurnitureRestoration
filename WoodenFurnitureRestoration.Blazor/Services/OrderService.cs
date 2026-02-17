using WoodenFurnitureRestoration.Shared.DTOs.Order;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class OrderService(HttpClient httpClient, ILogger<OrderService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/orders";

    public async Task<List<OrderDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all orders from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<OrderDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching orders");
            return [];
        }
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching order {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateOrderDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating order");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating order {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting order {Id}", id);
            return false;
        }
    }
}