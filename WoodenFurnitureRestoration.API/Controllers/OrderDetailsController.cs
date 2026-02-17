using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.OrderDetail;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Data.DbContextt;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController(
    IOrderDetailService orderDetailService,
    WoodenFurnitureRestorationContext context,
    IMapper mapper,
    ILogger<OrderDetailsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OrderDetailDto>>> GetAll()
    {
        // ✅ Include ile navigation property'leri yüklüyoruz
        var details = await context.OrderDetails
            .Include(od => od.Product)
            .Include(od => od.Restoration)
            .Include(od => od.Order)
            .Where(od => !od.Deleted)
            .ToListAsync();
        return Ok(mapper.Map<List<OrderDetailDto>>(details));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDetailDto>> GetById(int id)
    {
        var detail = await context.OrderDetails
            .Include(od => od.Product)
            .Include(od => od.Restoration)
            .Include(od => od.Order)
            .FirstOrDefaultAsync(od => od.Id == id && !od.Deleted);
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