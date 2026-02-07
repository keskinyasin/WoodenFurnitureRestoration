using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Order;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(
    IOrderService orderService,
    IMapper mapper,
    ILogger<OrdersController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAll()
    {
        var orders = await orderService.GetAllAsync();
        return Ok(mapper.Map<List<OrderDto>>(orders));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await orderService.GetByIdAsync(id);
        if (order is null) return NotFound(new { message = "Sipariş bulunamadı" });
        return Ok(mapper.Map<OrderDto>(order));
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var order = mapper.Map<Order>(dto);
        await orderService.CreateAsync(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, mapper.Map<OrderDto>(order));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await orderService.UpdateAsync(id, mapper.Map<Order>(dto));
        return result ? NoContent() : NotFound(new { message = "Sipariş bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await orderService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Sipariş bulunamadı" });
    }
}