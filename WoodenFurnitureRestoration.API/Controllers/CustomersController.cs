using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Customer;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(
    ICustomerService customerService,
    IMapper mapper,
    ILogger<CustomersController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> GetAll()
    {
        var customers = await customerService.GetAllAsync();
        return Ok(mapper.Map<List<CustomerDto>>(customers));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await customerService.GetByIdAsync(id);
        if (customer is null) return NotFound(new { message = "Müşteri bulunamadı" });
        return Ok(mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var customer = mapper.Map<Customer>(dto);
        await customerService.CreateAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, mapper.Map<CustomerDto>(customer));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await customerService.UpdateAsync(id, mapper.Map<Customer>(dto));
        return result ? NoContent() : NotFound(new { message = "Müşteri bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await customerService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Müşteri bulunamadı" });
    }
}