using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.RestorationService;
using RestorationServiceEntity = WoodenFurnitureRestoration.Entities.RestorationService;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestorationServicesController(
    IRestorationServiceService restorationServiceService,
    IMapper mapper,
    ILogger<RestorationServicesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RestorationServiceDto>>> GetAll()
    {
        var services = await restorationServiceService.GetAllAsync();
        return Ok(mapper.Map<List<RestorationServiceDto>>(services));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestorationServiceDto>> GetById(int id)
    {
        var service = await restorationServiceService.GetByIdAsync(id);
        if (service is null) return NotFound(new { message = "Hizmet bulunamadı" });
        return Ok(mapper.Map<RestorationServiceDto>(service));
    }

    [HttpPost]
    public async Task<ActionResult<RestorationServiceDto>> Create([FromBody] CreateRestorationServiceDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var service = mapper.Map<RestorationServiceEntity>(dto);
        await restorationServiceService.CreateAsync(service);
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, mapper.Map<RestorationServiceDto>(service));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRestorationServiceDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await restorationServiceService.UpdateAsync(id, mapper.Map<RestorationServiceEntity>(dto));
        return result ? NoContent() : NotFound(new { message = "Hizmet bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await restorationServiceService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Hizmet bulunamadı" });
    }
}