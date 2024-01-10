using Microsoft.AspNetCore.Mvc;
using HeartHousingAngular.Models;
using System.Net.WebSockets;
using HeartHousingAngular.DAL;

namespace HeartHousingAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController : Controller
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ILogger<RentalController> _logger;

    public RentalController(IRentalRepository rentalRepository, ILogger<RentalController> logger)
    {
        _rentalRepository = rentalRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRentals()
    {
        var rentals = await _rentalRepository.GetAllRentals();
        if (rentals == null)
        {
            _logger.LogError("[RentalController] Rental list not found while executing _rentalRepository.GetAllRentals");
            return NotFound("Rental list not found");
        }
        return Ok(rentals);
    }

    [HttpPost("createRental")]
    public async Task<IActionResult> CreateRental([FromBody] Rental newRental)
    {
        if (newRental == null)
        {
            return BadRequest("Invalid rental data");
        }
        bool returnOk = await _rentalRepository.CreateRental(newRental);

        if (returnOk)
        {
            var response = new { success = true, message = "Rental " + newRental.Name + " creates successfully" };
            return Ok(response);
        }
        else
        {
            var response = new { success = false, message = "Rental creation failed" };
            return Ok(response);
        }
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetRentalById (int id)
    {
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental == null)
        {
            _logger.LogError("[RentalController] Rental list not found while executing _rentalRepository.GetAll()");
            return NotFound("Rental list not found");
        }
        return Ok(rental);

    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Rental newRental)
    {
        if (newRental == null)
        {
            return BadRequest("Invalid rental data");
        }
        bool returnOk = await _rentalRepository.UpdateRental(newRental);
        if(returnOk)
        {
            var response = new { success = true, message = "Rental " + newRental.Name + " updated sucessfully" };
            return Ok(response);
        } else
        {
            var response = new { success = false, message = "Rental creation failed" };
            return Ok(response);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRental(int id)
    {
        bool returnOk = await _rentalRepository.DeleteRental(id);
        if (!returnOk)
        {
            _logger.LogError("[RentalController] Rental deletion failed for the RentalId {RentalId:0000}", id);
            return BadRequest("Rental deletion failed");
        }
        var response = new { success = true, message = "Item " + id.ToString() + "deleted successfully" };
        return Ok(response);
    }
}
