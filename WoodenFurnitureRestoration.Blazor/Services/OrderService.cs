using WoodenFurnitureRestoration.Shared.DTOs.Order;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;
        private const string ApiUrl = "https://localhost:5000/api/order";

        public OrderService(HttpClient httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all orders from API");
                var response = await _httpClient.GetAsync(ApiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API error: {StatusCode}", response.StatusCode);
                    return new List<OrderDto>();
                }

                var orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>();
                _logger.LogInformation("Successfully fetched {OrderCount} orders", orders?.Count ?? 0);
                return orders ?? new List<OrderDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders from API");
                return new List<OrderDto>();
            }
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching order with ID {OrderId} from API", id);
                var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found", id);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<OrderDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order with ID {OrderId}", id);
                return null;
            }
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto createDto)
        {
            try
            {
                _logger.LogInformation("Creating new order via API");
                var response = await _httpClient.PostAsJsonAsync(ApiUrl, createDto);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to create order: {StatusCode}", response.StatusCode);
                    return null;
                }

                var createdOrder = await response.Content.ReadFromJsonAsync<OrderDto>();
                _logger.LogInformation("Order created successfully with ID {OrderId}", createdOrder?.Id);
                return createdOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrderDto updateDto)
        {
            try
            {
                _logger.LogInformation("Updating order with ID {OrderId}", id);
                var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", updateDto);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to update order: {StatusCode}", response.StatusCode);
                    return false;
                }

                _logger.LogInformation("Order with ID {OrderId} updated successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order with ID {OrderId}", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting order with ID {OrderId}", id);
                var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to delete order: {StatusCode}", response.StatusCode);
                    return false;
                }

                _logger.LogInformation("Order with ID {OrderId} deleted successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order with ID {OrderId}", id);
                return false;
            }
        }
    }
}