using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.OrderDetail;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController(
    IOrderDetailService orderDetailService,
    IMapper mapper,
    ILogger<OrderDetailsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OrderDetailDto>>> GetAll()
    {
        var details = await orderDetailService.GetAllAsync();
        return Ok(mapper.Map<List<OrderDetailDto>>(details));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDetailDto>> GetById(int id)
    {
        var detail = await orderDetailService.GetByIdAsync(id);
        if (detail is null) return NotFound(new { message = "Sipariş detayı bulunamadı" });
        return Ok(mapper.Map<OrderDetailDto>(detail));
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetailDto>> Create([FromBody] CreateOrderDetailDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var detail = mapper.Map<OrderDetail>(dto);
        await orderDetailService.CreateAsync(detail);
        return CreatedAtAction(nameof(GetById), new { id = detail.Id }, mapper.Map<OrderDetailDto>(detail));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDetailDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await orderDetailService.UpdateAsync(id, mapper.Map<OrderDetail>(dto));
        return result ? NoContent() : NotFound(new { message = "Sipariş detayı bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await orderDetailService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Sipariş detayı bulunamadı" });
    }
}