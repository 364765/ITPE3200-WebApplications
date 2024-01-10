using HeartHousing.DAL;
using HeartHousing.Models;
using HeartHousing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeartHousing.Controllers;


public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly ILogger<OrderController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public OrderController(IOrderRepository orderRepository, IRentalRepository rentalRepository, ILogger<OrderController> logger, UserManager<IdentityUser> userManager)
    {
        _orderRepository = orderRepository;
        _rentalRepository = rentalRepository;
        _logger = logger;
        _userManager = userManager;

    }

    /*---------------Gets all orders by user and display it in Order/Table---------------------*/
    [Authorize]
    public async Task<IActionResult> Table()
    {
        //Finds the userID of currently logged in user
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //Gets all orders with the users ID
        var orders = await _orderRepository.GetAllOrdersByUserId(userId);

        if (orders == null)
        {
            _logger.LogError("[OrderController] Order list not found while executing _orderRepository.GetAllOrdersByUserId()");
            return NotFound("Order list not found");
        }

        //Displays all orders of the user in the table page
        var orderListViewModel = new OrderListViewModel(orders, "Table");
        return View(orderListViewModel);
    }


    /*---------------Creates order in Order/Create---------------------*/
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateOrder(int RentalID)
    {
        var createOrderRentalViewModel = new CreateOrderRentalViewModel
        {
            // Sets the view to the id of the rental the user want to order
            RentalID = RentalID,
        };
        return View(createOrderRentalViewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRentalViewModel createOrderRentalViewModel)
    {
        if (ModelState.IsValid)
        {
            // Gets the ID of the rental
            var rental = await _rentalRepository.GetRentalById(createOrderRentalViewModel.RentalID);

            // Finds ID of the logged in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Creates a new order and saves it to the user's ID
            var order = new Order
            {
                NightsNr = createOrderRentalViewModel.NightsNr,
                RentalID = createOrderRentalViewModel.RentalID,
                TotalPrice = createOrderRentalViewModel.NightsNr * rental.PricePrNigth,
                UserID = userId,

            };

            // Creates the order
            bool returnOk = await _orderRepository.CreateOrder(order);

            // Redirects to table overview of orders
            if (returnOk)
            {
                return RedirectToAction("Table");
            }

        }
        _logger.LogError("[OrderController] Order creation failed {@order}", createOrderRentalViewModel);

        return View(createOrderRentalViewModel);
    }

    /*--------------- Update order in Order/Table---------------------*/
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> updateOrder(int OrderID)
    {
        //Sets view to update order page of the given ID
        var updateOrderViewModel = new UpdateOrderViewModel
        {
            OrderID = OrderID,
        };

        if (updateOrderViewModel == null)
        {
            _logger.LogError("[OrderController] Order not found when updating the OrderId {OrderId:0000}", OrderID);
            return BadRequest("Order not found for the OrderId");
        }
        return View(updateOrderViewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpdateOrder(UpdateOrderViewModel updateOrderViewModel)
    {
        if (ModelState.IsValid)
        {
            var selectedOrder = await _orderRepository.GetOrderById(updateOrderViewModel.OrderID);

            //ID of current Rental
            int rentalID = selectedOrder.RentalID;

            //Gets the rental and informormation from repository with ID
            var rental = await _rentalRepository.GetRentalById(rentalID);

            //Sets DB table value
            selectedOrder.NightsNr = updateOrderViewModel.NightsNr;
            selectedOrder.TotalPrice = updateOrderViewModel.NightsNr * rental.PricePrNigth;

            // Updates order
            bool returnOk = await _orderRepository.UpdateOrder(selectedOrder);
            if (returnOk)
            {
                return RedirectToAction("Table");
            }
        }
        _logger.LogWarning("[OrderController] Order update failed {@order}", updateOrderViewModel);

        return View(updateOrderViewModel);
    }

    /*--------------- Deletes order in Order/Table---------------------*/
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        // Gets the ID of the order the user wants to delete and goes to confirm deletion page
        var order = await _orderRepository.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        if (order == null)
        {
            _logger.LogError("[OrderController] Order list not found for the OrderId {OrderId:0000}", id);
            return BadRequest("Order not found for the OrderId");
        }

        return View(order);

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteOrderConfirmed(int id)
    {
        // Gets the order ID
        var order = await _orderRepository.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }

        // Deletes order
        bool returnOk = await _orderRepository.DeleteOrder(id);
        if (!returnOk)
        {
            _logger.LogError("[OrderController] Order deletion failed for the OrderId {OrderId:0000}", id);
            return BadRequest("Order deletion failed");
        }

        await _orderRepository.DeleteOrder(id);
        return RedirectToAction("Table"); 

    }


}