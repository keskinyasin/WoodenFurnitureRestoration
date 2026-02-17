using WoodenFurnitureRestoration.Shared.DTOs.Invoice;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services;

public class InvoiceService(HttpClient httpClient, ILogger<InvoiceService> logger)
{
    private const string ApiUrl = "https://localhost:7130/api/invoices";

    public async Task<List<InvoiceDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("📥 Fetching all invoices from API");
            var response = await httpClient.GetAsync(ApiUrl);
            if (!response.IsSuccessStatusCode) return [];
            return await response.Content.ReadFromJsonAsync<List<InvoiceDto>>() ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching invoices");
            return [];
        }
    }

    public async Task<InvoiceDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<InvoiceDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error fetching invoice {Id}", id);
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreateInvoiceDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrl, dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error creating invoice");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateInvoiceDto dto)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error updating invoice {Id}", id);
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
            logger.LogError(ex, "❌ Error deleting invoice {Id}", id);
            return false;
        }
    }
}