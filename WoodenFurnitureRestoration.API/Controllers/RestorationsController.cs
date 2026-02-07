using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Restoration;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestorationsController(
    IRestorationService restorationService,
    IMapper mapper,
    ILogger<RestorationsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RestorationDto>>> GetAll()
    {
        var restorations = await restorationService.GetAllAsync();
        return Ok(mapper.Map<List<RestorationDto>>(restorations));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestorationDto>> GetById(int id)
    {
        var restoration = await restorationService.GetByIdAsync(id);
        if (restoration is null) return NotFound(new { message = "Restorasyon bulunamadı" });
        return Ok(mapper.Map<RestorationDto>(restoration));
    }

    [HttpPost]
    public async Task<ActionResult<RestorationDto>> Create([FromBody] CreateRestorationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var restoration = mapper.Map<Restoration>(dto);
        await restorationService.CreateAsync(restoration);
        return CreatedAtAction(nameof(GetById), new { id = restoration.Id }, mapper.Map<RestorationDto>(restoration));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRestorationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await restorationService.UpdateAsync(id, mapper.Map<Restoration>(dto));
        return result ? NoContent() : NotFound(new { message = "Restorasyon bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await restorationService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Restorasyon bulunamadı" });
    }
}