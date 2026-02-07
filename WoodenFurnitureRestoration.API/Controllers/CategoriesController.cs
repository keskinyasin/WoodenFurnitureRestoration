using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Category;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(
        ICategoryService categoryService,
        IMapper mapper,
        ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        try
        {
            _logger.LogInformation("📥 Fetching all categories");
            var categories = await _categoryService.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error fetching categories");
            return StatusCode(500, new { message = "Kategoriler alınırken hata oluştu", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category is null) return NotFound(new { message = "Kategori bulunamadı" });
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var category = _mapper.Map<Category>(dto);
        await _categoryService.CreateAsync(category);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, _mapper.Map<CategoryDto>(category));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var category = _mapper.Map<Category>(dto);
        var result = await _categoryService.UpdateAsync(id, category);
        return result ? NoContent() : NotFound(new { message = "Kategori bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Kategori bulunamadı" });
    }
}