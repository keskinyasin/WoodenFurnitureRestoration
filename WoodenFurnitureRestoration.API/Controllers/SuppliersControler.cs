using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Supplier;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController(
    ISupplierService supplierService,
    IMapper mapper,
    ILogger<SuppliersController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<SupplierDto>>> GetAll()
    {
        var suppliers = await supplierService.GetAllAsync();
        return Ok(mapper.Map<List<SupplierDto>>(suppliers));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupplierDto>> GetById(int id)
    {
        var supplier = await supplierService.GetByIdAsync(id);
        if (supplier is null) return NotFound(new { message = "Tedarikçi bulunamadı" });
        return Ok(mapper.Map<SupplierDto>(supplier));
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var supplier = mapper.Map<Supplier>(dto);
        await supplierService.CreateAsync(supplier);
        return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, mapper.Map<SupplierDto>(supplier));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await supplierService.UpdateAsync(id, mapper.Map<Supplier>(dto));
        return result ? NoContent() : NotFound(new { message = "Tedarikçi bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await supplierService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Tedarikçi bulunamadı" });
    }
}