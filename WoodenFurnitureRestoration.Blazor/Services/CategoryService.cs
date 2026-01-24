using WoodenFurnitureRestoration.Shared.DTOs.Category;

namespace WoodenFurnitureRestoration.Blazor.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CategoryService> _logger;
        private const string API_URL = "https://localhost:7001/api/categories";

        public CategoryService(HttpClient httpClient, ILogger<CategoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("📥 Fetching categories from API...");
                var response = await _httpClient.GetAsync(API_URL);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"📦 Response: {json}");

                    var categories = System.Text.Json.JsonSerializer.Deserialize<List<CategoryDto>>(
                        json,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    ) ?? new();

                    _logger.LogInformation($"✅ Got {categories.Count} categories");
                    return categories;
                }
                else
                {
                    _logger.LogWarning($"⚠️ API returned {response.StatusCode}");
                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error fetching categories");
                return new();
            }
        }

        public async Task<bool> CreateAsync(CreateCategoryDto dto)
        {
            try
            {
                _logger.LogInformation($"📤 Creating category: {dto.Name}");

                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(dto),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync(API_URL, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("✅ Category created successfully");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"⚠️ Creation failed: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error creating category");
                return false;
            }
        }
    }
}