using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Product;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(
    IProductService productService,
    IMapper mapper,
    ILogger<ProductsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products = await productService.GetAllAsync();
        return Ok(mapper.Map<List<ProductDto>>(products));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await productService.GetByIdAsync(id);
        if (product is null) return NotFound(new { message = "Ürün bulunamadı" });
        return Ok(mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var product = mapper.Map<Product>(dto);
        await productService.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, mapper.Map<ProductDto>(product));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await productService.UpdateAsync(id, mapper.Map<Product>(dto));
        return result ? NoContent() : NotFound(new { message = "Ürün bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await productService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Ürün bulunamadı" });
    }
}