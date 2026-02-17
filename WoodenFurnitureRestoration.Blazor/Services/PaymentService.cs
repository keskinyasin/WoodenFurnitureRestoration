using WoodenFurnitureRestoration.Shared.DTOs.Payment;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class PaymentService(HttpClient httpClient, ILogger<PaymentService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/payments";

    public async Task<List<PaymentDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all payments from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<PaymentDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching payments");
            return [];
        }
    }

    public async Task<PaymentDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<PaymentDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching payment {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreatePaymentDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating payment");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdatePaymentDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating payment {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting payment {Id}", id);
            return false;
        }
    }
}