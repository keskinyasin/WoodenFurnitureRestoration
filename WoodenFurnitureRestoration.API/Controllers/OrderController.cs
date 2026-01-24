using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WoodenFurnitureRestoration.Shared.DTOs.Order;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;  // ✅ Ekle

        public OrderController(ILogger<OrderController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;  // ✅ Enjekte et
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all orders");
                var orders = await _unitOfWork.Orders.GetAllAsync();

                // ✅ AutoMapper ile dönüştür
                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching orders");
                return StatusCode(500, new { message = "Error occurred while fetching orders", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching order with ID {OrderId}", id);
                var order = await _unitOfWork.Orders.FindAsync(id);
                if (order == null)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found", id);
                    return NotFound(new { message = "Order not found" });
                }

                // ✅ AutoMapper ile dönüştür
                var orderDto = _mapper.Map<OrderDto>(order);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching order with ID {OrderId}", id);
                return StatusCode(500, new { message = "Error occurred while fetching order", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto createDto)
        {
            try
            {
                _logger.LogInformation("Creating a new order with status: {OrderStatus}", createDto.Status);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for order creation");
                    return BadRequest(new { message = "Model geçersiz", errors = ModelState });
                }

                if (string.IsNullOrWhiteSpace(createDto.Status))
                {
                    _logger.LogWarning("Order status is required");
                    ModelState.AddModelError(nameof(createDto.Status), "Order status is required");
                    return BadRequest(ModelState);
                }

                // ✅ AutoMapper ile dönüştür
                var order = _mapper.Map<Order>(createDto);
                order.CreatedDate = DateTime.UtcNow;
                order.UpdatedDate = DateTime.UtcNow;
                order.Deleted = false;

                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Order created successfully with ID: {OrderId}", order.Id);

                // ✅ AutoMapper ile dönüştür
                var orderDto = _mapper.Map<OrderDto>(order);

                return CreatedAtAction(nameof(GetById), new { id = order.Id }, orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order");
                return StatusCode(500, new { message = "Error occurred while creating order", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto updateDto)
        {
            try
            {
                _logger.LogInformation("Updating order with ID: {OrderId}", id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for order update (ID: {OrderId})", id);
                    return BadRequest(new { message = "Model geçersiz", errors = ModelState });
                }

                var existingOrder = await _unitOfWork.Orders.FindAsync(id);
                if (existingOrder == null)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found for update", id);
                    return NotFound(new { message = "Order not found" });
                }

                if (!string.IsNullOrEmpty(updateDto.Status))
                    existingOrder.OrderStatus = updateDto.Status;

                if (updateDto.ShippingId.HasValue)
                    existingOrder.ShippingId = updateDto.ShippingId.Value;

                existingOrder.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Orders.Update(existingOrder);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Order with ID {OrderId} updated successfully", id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order with ID {OrderId}", id);
                return StatusCode(500, new { message = "Error occurred while updating order", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting order with ID: {OrderId}", id);
                var existingOrder = await _unitOfWork.Orders.FindAsync(id);
                if (existingOrder == null)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found for deletion", id);
                    return NotFound(new { message = "Order not found" });
                }

                _unitOfWork.Orders.Delete(existingOrder);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Order with ID {OrderId} deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting order with ID {OrderId}", id);
                return StatusCode(500, new { message = "Error occurred while deleting order", error = ex.Message });
            }
        }
    }
}