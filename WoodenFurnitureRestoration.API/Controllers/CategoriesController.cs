using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Shared.DTOs.Category;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;  // ✅ Ekle

        public CategoriesController(ILogger<CategoriesController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;  // ✅ Enjekte et
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all categories");
                var categories = await _unitOfWork.Categories.GetAllAsync();

                // ✅ AutoMapper ile dönüştür
                var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriler alınırken hata oluştu");
                return StatusCode(500, new { message = "Kategoriler alınırken hata oluştu", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.FindAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found", id);
                    return NotFound(new { message = "Kategori bulunamadı" });
                }

                // ✅ AutoMapper ile dönüştür
                var categoryDto = _mapper.Map<CategoryDto>(category);

                Console.WriteLine($"DTO Name: {categoryDto.Name}");
                Console.WriteLine($"DTO Type: {categoryDto.GetType().Name}");

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori alınırken hata oluştu (ID: {CategoryId})", id);
                return StatusCode(500, new { message = "Hata oluştu", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto createDto)
        {
            try
            {
                _logger.LogInformation($"Creating a new category: {createDto.Name}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for category creation");
                    return BadRequest(new { message = "Model geçersiz", errors = ModelState });
                }

                if (string.IsNullOrWhiteSpace(createDto.Name))
                {
                    _logger.LogWarning("Category name is required");
                    ModelState.AddModelError(nameof(createDto.Name), "Kategori adı gereklidir");
                    return BadRequest(ModelState);
                }

                // ✅ AutoMapper ile dönüştür
                var category = _mapper.Map<Category>(createDto);
                category.CreatedDate = DateTime.UtcNow;
                category.UpdatedDate = DateTime.UtcNow;
                category.Deleted = false;

                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Category created with ID: {category.Id}, Name={category.CategoryName}");

                // ✅ AutoMapper ile dönüştür
                var categoryDto = _mapper.Map<CategoryDto>(category);

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori oluşturulurken hata oluştu");
                return StatusCode(500, new { message = "Kategori oluşturulurken hata oluştu", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            try
            {
                _logger.LogInformation("Updating category with ID: {CategoryId}", id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for category update (ID: {CategoryId})", id);
                    return BadRequest(new { message = "Model geçersiz", errors = ModelState });
                }

                var existingCategory = await _unitOfWork.Categories.FindAsync(id);
                if (existingCategory == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for update", id);
                    return NotFound(new { message = "Kategori bulunamadı" });
                }

                if (!string.IsNullOrEmpty(categoryDto.Name))
                    existingCategory.CategoryName = categoryDto.Name;

                if (!string.IsNullOrEmpty(categoryDto.Description))
                    existingCategory.CategoryDescription = categoryDto.Description;

                existingCategory.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Categories.Update(existingCategory);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Category updated successfully (ID: {CategoryId}, Name: {CategoryName})", id, categoryDto.Name);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori güncellenirken hata oluştu (ID: {CategoryId})", id);
                return StatusCode(500, new { message = "Kategori güncellenirken hata oluştu", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting category with ID: {CategoryId}", id);
                var existingCategory = await _unitOfWork.Categories.FindAsync(id);
                if (existingCategory == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for deletion", id);
                    return NotFound(new { message = "Kategori bulunamadı" });
                }
                _unitOfWork.Categories.Delete(existingCategory);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Category deleted successfully (ID: {CategoryId}, Name: {CategoryName})", id, existingCategory.CategoryName);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori silinirken hata oluştu (ID: {CategoryId})", id);
                return StatusCode(500, new { message = "Kategori silinirken hata oluştu", error = ex.Message });
            }
        }
    }
}