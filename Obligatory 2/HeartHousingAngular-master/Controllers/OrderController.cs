using HeartHousingAngular.DAL;
using HeartHousingAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeartHousingAngular.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetallOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        if (orders == null)
        {
            _logger.LogError("[OrderController] Order list not found while executing _orderRepository.GetAllOrders");
            return NotFound("Order list not found");
        }
        return Ok(orders);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] Order newOrder)
    {
        if (newOrder == null)
        {
            return BadRequest("Invalid order data");
        }
        bool returnOk = await _orderRepository.CreateOrder(newOrder);

        if (returnOk)
        {
            var response = new { success = true, message = "Order " + newOrder.OrderId + " creates successfully" };
            return Ok(response);
        }
        else
        {
            var response = new { success = false, message = "Order creation failed" };
            return Ok(response);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderById(id);
        if (order == null)
        {
            _logger.LogError("[OrderController] Order list not found while executing _orderRepository.GetAll()");
            return NotFound("Order list not found");
        }
        return Ok(order);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateOrder(Order newOrder)
    {
        if (newOrder == null)
        {
            return BadRequest("Invalid order data");
        }
        bool returnOk = await _orderRepository.UpdateOrder(newOrder);
        if (returnOk)
        {
            var response = new { success = true, message = "Order " + newOrder.OrderId + " updated sucessfully" };
            return Ok(response);
        }
        else
        {
            var response = new { success = false, message = "Order creation failed" };
            return Ok(response);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        bool returnOk = await _orderRepository.DeleteOrder(id);
        if (!returnOk)
        {
            _logger.LogError("[OrderController] Order deletion failed for the OrderId {OrderId:0000}", id);
            return BadRequest("Order deletion failed");
        }
        var response = new { success = true, message = "Order " + id.ToString() + "deleted successfully" };
        return Ok(response);
    }

}