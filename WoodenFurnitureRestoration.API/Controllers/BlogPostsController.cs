using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.BlogPost;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController(
    IBlogPostService blogPostService,
    IMapper mapper,
    ILogger<BlogPostsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BlogPostDto>>> GetAll()
    {
        var posts = await blogPostService.GetAllAsync();
        return Ok(mapper.Map<List<BlogPostDto>>(posts));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BlogPostDto>> GetById(int id)
    {
        var post = await blogPostService.GetByIdAsync(id);
        if (post is null) return NotFound(new { message = "Blog yazısı bulunamadı" });
        return Ok(mapper.Map<BlogPostDto>(post));
    }

    [HttpPost]
    public async Task<ActionResult<BlogPostDto>> Create([FromBody] CreateBlogPostDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var post = mapper.Map<BlogPost>(dto);
        await blogPostService.CreateAsync(post);
        return CreatedAtAction(nameof(GetById), new { id = post.Id }, mapper.Map<BlogPostDto>(post));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBlogPostDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await blogPostService.UpdateAsync(id, mapper.Map<BlogPost>(dto));
        return result ? NoContent() : NotFound(new { message = "Blog yazısı bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await blogPostService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Blog yazısı bulunamadı" });
    }
}