using WoodenFurnitureRestoration.Shared.DTOs.Address;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class AddressService(HttpClient httpClient, ILogger<AddressService> logger)
{
    private const string ApiUrl = "https://localhost:7265/api/addresses";

    public async Task<List<AddressDto>> GetAllAsync()

    {
        try
        {
            logger.LogInformation("📥 Fetching all addresses from API");
            var response = await httpClient.GetAsync(ApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("❌ API error: {StatusCode}", response.StatusCode);
                return [];
            }

            var addresses = await response.Content.ReadFromJsonAsync<List<AddressDto>>();
            logger.LogInformation("✅ Successfully fetched {Count} addresses", addresses?.Count ?? 0);
            return addresses ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching addresses");
            return [];
        }
    }

    public async Task<AddressDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AddressDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching address {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateAddressDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating address");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateAddressDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating address {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting address {Id}", id);
            return false;
        }
    }
}