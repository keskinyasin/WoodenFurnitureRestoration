using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Shared.DTOs.Address;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController(
    IAddressService addressService,
    IMapper mapper,
    ILogger<AddressesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AddressDto>>> GetAll()
    {
        try
        {
            var addresses = await addressService.GetAllAsync();
            return Ok(mapper.Map<List<AddressDto>>(addresses));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching addresses");
            return StatusCode(500, new { message = "Adresler alınırken hata oluştu" });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AddressDto>> GetById(int id)
    {
        var address = await addressService.GetByIdAsync(id);
        if (address is null) return NotFound(new { message = "Adres bulunamadı" });
        return Ok(mapper.Map<AddressDto>(address));
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> Create([FromBody] CreateAddressDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var address = mapper.Map<Address>(dto);
        await addressService.CreateAsync(address);
        return CreatedAtAction(nameof(GetById), new { id = address.Id }, mapper.Map<AddressDto>(address));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await addressService.UpdateAsync(id, mapper.Map<Address>(dto));
        return result ? NoContent() : NotFound(new { message = "Adres bulunamadı" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await addressService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { message = "Adres bulunamadı" });
    }
}