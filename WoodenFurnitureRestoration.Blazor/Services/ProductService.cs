using WoodenFurnitureRestoration.Shared.DTOs.Product;
using System.Net.Http.Json;

namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;
        private const string ApiUrl = "https://localhost:5000/api/products";

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all products from API");
                var response = await _httpClient.GetAsync(ApiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API error: {StatusCode}", response.StatusCode);
                    return new List<ProductDto>();
                }

                var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                _logger.LogInformation("Successfully fetched {ProductCount} products", products?.Count ?? 0);
                return products ?? new List<ProductDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products from API");
                return new List<ProductDto>();
            }
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching product with ID {ProductId} from API", id);
                var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product with ID {ProductId}", id);
                return null;
            }
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createDto)
        {
            try
            {
                _logger.LogInformation("Creating new product via API");
                var response = await _httpClient.PostAsJsonAsync(ApiUrl, createDto);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to create product: {StatusCode}", response.StatusCode);
                    return null;
                }

                var createdProduct = await response.Content.ReadFromJsonAsync<ProductDto>();
                _logger.LogInformation("Product created successfully with ID {ProductId}", createdProduct?.Id);
                return createdProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto updateDto)
        {
            try
            {
                _logger.LogInformation("Updating product with ID {ProductId}", id);
                var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", updateDto);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to update product: {StatusCode}", response.StatusCode);
                    return false;
                }

                _logger.LogInformation("Product with ID {ProductId} updated successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {ProductId}", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting product with ID {ProductId}", id);
                var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to delete product: {StatusCode}", response.StatusCode);
                    return false;
                }

                _logger.LogInformation("Product with ID {ProductId} deleted successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {ProductId}", id);
                return false;
            }
        }
    }
}