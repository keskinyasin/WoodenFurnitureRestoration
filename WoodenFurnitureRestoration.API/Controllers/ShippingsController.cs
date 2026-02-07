using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Shipping;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShippingsController(
    IShippingService shippingService,
    IMapper mapper,
    ILogger<ShippingsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ShippingDto>>> GetAll()
    {
        var shippings = await shippingService.GetAllAsync();
        return Ok(mapper.Map<List<ShippingDto>>(shippings));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShippingDto>> GetById(int id)
    {
        var shipping = await shippingService.GetByIdAsync(id);
        if (shipping is null) return NotFound(new { message = "Kargo bulunamadı" });
        return Ok(mapper.Map<ShippingDto>(shipping));
    }

    [HttpPost]
    public async Task<ActionResult<ShippingDto>> Create([FromBody] CreateShippingDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var shipping = mapper.Map<Shipping>(dto);
        await shippingService.CreateAsync(shipping);
        return CreatedAtAction(nameof(GetById), new { id = shipping.Id }, mapper.Map<ShippingDto>(shipping));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateShippingDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await shippingService.UpdateAsync(id, mapper.Map<Shipping>(dto));
        return result ? NoContent() : NotFound(new { message = "Kargo bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await shippingService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Kargo bulunamadı" });
    }
}