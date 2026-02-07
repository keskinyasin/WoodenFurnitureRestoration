using WoodenFurnitureRestoration.Shared.DTOs.OrderDetail;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class OrderDetailService(HttpClient httpClient, ILogger<OrderDetailService> logger)
{
    private const string ApiUrl = "https://localhost:7001/api/orderdetails";

    public async Task<List<OrderDetailDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all order details from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<OrderDetailDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching order details");
            return [];
        }
    }

    public async Task<OrderDetailDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<OrderDetailDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching order detail {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateOrderDetailDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating order detail");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateOrderDetailDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating order detail {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting order detail {Id}", id);
            return false;
        }
    }
}