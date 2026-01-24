using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Shared.DTOs.Product;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;  // ✅ Ekle

        public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;  // ✅ Enjekte et
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all products");
                var products = await _unitOfWork.Products.GetAllAsync();

                // ✅ AutoMapper ile dönüştür
                var productDtos = _mapper.Map<List<ProductDto>>(products);

                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürünler alınırken hata oluştu");
                return StatusCode(500, new { message = "Ürünler alınırken hata oluştu", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching product with ID {ProductId}", id);
                var product = await _unitOfWork.Products.FindAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                    return NotFound(new { message = "Ürün bulunamadı" });
                }

                // ✅ AutoMapper ile dönüştür
                var productDto = _mapper.Map<ProductDto>(product);

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün alınırken hata oluştu (ID: {ProductId})", id);
                return StatusCode(500, new { message = "Hata oluştu", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto createDto)
        {
            try
            {
                _logger.LogInformation($"Creating a new product: {createDto.Name}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for product creation");
                    return BadRequest(new { message = "Model geçersiz", errors = ModelState });
                }

                if (string.IsNullOrWhiteSpace(createDto.Name))
                {
                    _logger.LogWarning("Product name is required");
                    ModelState.AddModelError(nameof(createDto.Name), "Ürün adı gereklidir");
                    return BadRequest(ModelState);
                }

                // ✅ AutoMapper ile dönüştür
                var product = _mapper.Map<Product>(createDto);
                product.CreatedDate = DateTime.UtcNow;
                product.UpdatedDate = DateTime.UtcNow;
                product.Deleted = false;

                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Product created with ID: {product.Id}, Name={product.ProductName}");

                // ✅ AutoMapper ile dönüştür
                var productDto = _mapper.Map<ProductDto>(product);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün oluşturulurken hata oluştu");
                return StatusCode(500, new { message = "Ürün oluşturulurken hata oluştu", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateDto)
        {
            try
            {
                _logger.LogInformation("Updating product with ID: {ProductId}", id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for product update (ID: {ProductId})", id);
                    return BadRequest(new { message = "Model geçersiz", errors = ModelState });
                }

                var existingProduct = await _unitOfWork.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for update", id);
                    return NotFound(new { message = "Ürün bulunamadı" });
                }

                if (!string.IsNullOrEmpty(updateDto.Name))
                    existingProduct.ProductName = updateDto.Name;

                if (updateDto.Price.HasValue && updateDto.Price > 0)
                    existingProduct.Price = updateDto.Price.Value;

                if (!string.IsNullOrEmpty(updateDto.Description))
                    existingProduct.Description = updateDto.Description;

                existingProduct.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Products.Update(existingProduct);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Product updated successfully (ID: {ProductId})", id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün güncellenirken hata oluştu (ID: {ProductId})", id);
                return StatusCode(500, new { message = "Ürün güncellenirken hata oluştu", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting product with ID {ProductId}", id);
                var existingProduct = await _unitOfWork.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for deletion", id);
                    return NotFound(new { message = "Ürün bulunamadı" });
                }

                _unitOfWork.Products.Delete(existingProduct);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Product deleted successfully (ID: {ProductId})", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün silinirken hata oluştu (ID: {ProductId})", id);
                return StatusCode(500, new { message = "Ürün silinirken hata oluştu", error = ex.Message });
            }
        }
    }
}