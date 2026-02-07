using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Inventory;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController(
    IInventoryService inventoryService,
    IMapper mapper,
    ILogger<InventoriesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InventoryDto>>> GetAll()
    {
        var inventories = await inventoryService.GetAllAsync();
        return Ok(mapper.Map<List<InventoryDto>>(inventories));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InventoryDto>> GetById(int id)
    {
        var inventory = await inventoryService.GetByIdAsync(id);
        if (inventory is null) return NotFound(new { message = "Envanter bulunamadı" });
        return Ok(mapper.Map<InventoryDto>(inventory));
    }

    [HttpPost]
    public async Task<ActionResult<InventoryDto>> Create([FromBody] CreateInventoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var inventory = mapper.Map<Inventory>(dto);
        await inventoryService.CreateAsync(inventory);
        return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, mapper.Map<InventoryDto>(inventory));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInventoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await inventoryService.UpdateAsync(id, mapper.Map<Inventory>(dto));
        return result ? NoContent() : NotFound(new { message = "Envanter bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await inventoryService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Envanter bulunamadı" });
    }
}