using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Invoice;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController(
    IInvoiceService invoiceService,
    IMapper mapper,
    ILogger<InvoicesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InvoiceDto>>> GetAll()
    {
        var invoices = await invoiceService.GetAllAsync();
        return Ok(mapper.Map<List<InvoiceDto>>(invoices));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceDto>> GetById(int id)
    {
        var invoice = await invoiceService.GetByIdAsync(id);
        if (invoice is null) return NotFound(new { message = "Fatura bulunamadı" });
        return Ok(mapper.Map<InvoiceDto>(invoice));
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> Create([FromBody] CreateInvoiceDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var invoice = mapper.Map<Invoice>(dto);
        await invoiceService.CreateAsync(invoice);
        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, mapper.Map<InvoiceDto>(invoice));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await invoiceService.UpdateAsync(id, mapper.Map<Invoice>(dto));
        return result ? NoContent() : NotFound(new { message = "Fatura bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await invoiceService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Fatura bulunamadı" });
    }
}