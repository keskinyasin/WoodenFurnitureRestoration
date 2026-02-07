using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Review;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(
    IReviewService reviewService,
    IMapper mapper,
    ILogger<ReviewsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ReviewDto>>> GetAll()
    {
        var reviews = await reviewService.GetAllAsync();
        return Ok(mapper.Map<List<ReviewDto>>(reviews));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReviewDto>> GetById(int id)
    {
        var review = await reviewService.GetByIdAsync(id);
        if (review is null) return NotFound(new { message = "Yorum bulunamadı" });
        return Ok(mapper.Map<ReviewDto>(review));
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create([FromBody] CreateReviewDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var review = mapper.Map<Review>(dto);
        await reviewService.CreateAsync(review);
        return CreatedAtAction(nameof(GetById), new { id = review.Id }, mapper.Map<ReviewDto>(review));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await reviewService.UpdateAsync(id, mapper.Map<Review>(dto));
        return result ? NoContent() : NotFound(new { message = "Yorum bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await reviewService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Yorum bulunamadı" });
    }
}