using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Tag;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController(
    ITagService tagService,
    IMapper mapper,
    ILogger<TagsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetAll()
    {
        var tags = await tagService.GetAllAsync();
        return Ok(mapper.Map<List<TagDto>>(tags));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TagDto>> GetById(int id)
    {
        var tag = await tagService.GetByIdAsync(id);
        if (tag is null) return NotFound(new { message = "Etiket bulunamadı" });
        return Ok(mapper.Map<TagDto>(tag));
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> Create([FromBody] CreateTagDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var tag = mapper.Map<Tag>(dto);
        await tagService.CreateAsync(tag);
        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, mapper.Map<TagDto>(tag));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTagDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await tagService.UpdateAsync(id, mapper.Map<Tag>(dto));
        return result ? NoContent() : NotFound(new { message = "Etiket bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await tagService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Etiket bulunamadı" });
    }
}