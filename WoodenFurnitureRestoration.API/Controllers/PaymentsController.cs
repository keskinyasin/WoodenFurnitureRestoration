using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Payment;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(
    IPaymentService paymentService,
    IMapper mapper,
    ILogger<PaymentsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PaymentDto>>> GetAll()
    {
        var payments = await paymentService.GetAllAsync();
        return Ok(mapper.Map<List<PaymentDto>>(payments));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaymentDto>> GetById(int id)
    {
        var payment = await paymentService.GetByIdAsync(id);
        if (payment is null) return NotFound(new { message = "Ödeme bulunamadı" });
        return Ok(mapper.Map<PaymentDto>(payment));
    }

    [HttpPost]
    public async Task<ActionResult<PaymentDto>> Create([FromBody] CreatePaymentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var payment = mapper.Map<Payment>(dto);
        await paymentService.CreateAsync(payment);
        return CreatedAtAction(nameof(GetById), new { id = payment.Id }, mapper.Map<PaymentDto>(payment));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await paymentService.UpdateAsync(id, mapper.Map<Payment>(dto));
        return result ? NoContent() : NotFound(new { message = "Ödeme bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await paymentService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Ödeme bulunamadı" });
    }
}