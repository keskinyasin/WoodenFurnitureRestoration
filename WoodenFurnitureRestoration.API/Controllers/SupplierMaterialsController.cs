using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.SupplierMaterial;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierMaterialsController(
    ISupplierMaterialService supplierMaterialService,
    IMapper mapper,
    ILogger<SupplierMaterialsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<SupplierMaterialDto>>> GetAll()
    {
        var materials = await supplierMaterialService.GetAllAsync();
        return Ok(mapper.Map<List<SupplierMaterialDto>>(materials));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupplierMaterialDto>> GetById(int id)
    {
        var material = await supplierMaterialService.GetByIdAsync(id);
        if (material is null) return NotFound(new { message = "Malzeme bulunamadı" });
        return Ok(mapper.Map<SupplierMaterialDto>(material));
    }

    [HttpPost]
    public async Task<ActionResult<SupplierMaterialDto>> Create([FromBody] CreateSupplierMaterialDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var material = mapper.Map<SupplierMaterial>(dto);
        await supplierMaterialService.CreateAsync(material);
        return CreatedAtAction(nameof(GetById), new { id = material.Id }, mapper.Map<SupplierMaterialDto>(material));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierMaterialDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await supplierMaterialService.UpdateAsync(id, mapper.Map<SupplierMaterial>(dto));
        return result ? NoContent() : NotFound(new { message = "Malzeme bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await supplierMaterialService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Malzeme bulunamadı" });
    }
}