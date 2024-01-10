using System.Security.Claims;
using HeartHousing.DAL;
using HeartHousing.Models;
using HeartHousing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace HeartHousing.Controllers;

public class RentalController : Controller
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ILogger<RentalController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public RentalController(IRentalRepository rentalRepository, ILogger<RentalController> logger,
        UserManager<IdentityUser> userManager)
    {
        _rentalRepository = rentalRepository;
        _logger = logger;
        _userManager = userManager;
    }

    /*------------Gets one Rental by id, and displays the detailed view of said rental----------*/
    public async Task<IActionResult> Details(int id)
    {
        //Gets the rental with the id
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental == null)
        {
            //if rental null, shows an error page, and in the log
            _logger.LogError("[RentalController] Rental not found for the RentalId {RentalId:0000}", id);
            return NotFound("Rental not found for the RentalId");
        }
        //Goes to the rental detailed view if rental was found
        return View(rental);
    }

    /*---------------Gets all rentals by user and display it in Rental/Table---------------------*/
    [Authorize]
    public async Task<IActionResult> Table()
    {
        //Finds the userID of currently logged in user
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //Gets all rentals with the users ID
        var rentals = await _rentalRepository.GetAllRentalsByUserId(userId);

        if (rentals == null)
        {
            _logger.LogError("[RentalController] Rental list not found while executing _rentalRepository.GetAllRentalsByUserId()");
            return NotFound("Rental list not found");
        }

        //Displays all rentals of the user in the table page
        var rentalListViewModel = new RentalListViewModel(rentals, "Table");
        return View(rentalListViewModel);
    }

    /*---------------Creates rental in Rental/Create---------------------*/
    [Authorize]
    [HttpGet]
    public IActionResult CreateRental()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRental(CreateRentalViewModel createRentalViewModel)
    {
        if (ModelState.IsValid)
        {
            // Finds ID of the logged in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Creates a new rental and saves it to the user's ID
            var rental = new Rental
            {
                Name = createRentalViewModel.Name,
                Address = createRentalViewModel.Address,
                PricePrNigth = createRentalViewModel.PricePrNight,
                RentalType = createRentalViewModel.RentalType,
                BedNr = createRentalViewModel.BedNr,
                BathNr = createRentalViewModel.BathNr,
                Area = createRentalViewModel.Area,
                Description = createRentalViewModel.Description,
                ImgUrl = createRentalViewModel.ImgUrl,
                ImgUrl2 = createRentalViewModel.ImgUrl2,
                ImgUrl3 = createRentalViewModel.ImgUrl,
                UserID = userId
            };

            // Creates the rental
            bool returnOk = await _rentalRepository.CreateRental(rental);

            // Redirects to table overview of orders
            if (returnOk)
            {
                return Redirect("Table"); 
            }
        }
        _logger.LogWarning("[RentalController] Rental creation failed {@rental}", createRentalViewModel);
        return View(createRentalViewModel);
    }

    /*--------------- Update rental in Rental/Table---------------------*/
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> UpdateRental(int rentalID)
    {
        //Sets view to update rental page of the given ID
        var updateRentalViewModel = new UpdateRentalViewModel
        {
            RentalID = rentalID,
        };
        if (updateRentalViewModel == null)
        {
            
            _logger.LogError("[RentalController] Rental not found when updating the RentalId {RentalId:0000}",
                rentalID);
            return BadRequest("Rental not found for the RentalId");
        }
        return View(updateRentalViewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpdateRental(UpdateRentalViewModel updateRentalViewModel)
    {
        if (ModelState.IsValid)
        {
            //ID of selected Rental
            var selectedRental = await _rentalRepository.GetRentalById(updateRentalViewModel.RentalID);
            
            // Sets DB table values of that rental
            selectedRental.Name = updateRentalViewModel.Name;
            selectedRental.Address = updateRentalViewModel.Address;
            selectedRental.PricePrNigth = updateRentalViewModel.PricePrNight;
            selectedRental.RentalType = updateRentalViewModel.RentalType;
            selectedRental.BedNr = updateRentalViewModel.BedNr;
            selectedRental.BathNr = updateRentalViewModel.BathNr;
            selectedRental.Area = updateRentalViewModel.Area;
            selectedRental.Description = updateRentalViewModel.Description;
            selectedRental.ImgUrl = updateRentalViewModel.ImgUrl;
            selectedRental.ImgUrl2 = updateRentalViewModel.ImgUrl2;
            selectedRental.ImgUrl3 = updateRentalViewModel.ImgUrl3;
            
            // Updates rental
            bool returnOk = await _rentalRepository.UpdateRental(selectedRental);
            
            if (returnOk)
            {
                return Redirect("Table"); 
            }
        }

        
        _logger.LogWarning("[RentalController] Rental update failed {@rental}", updateRentalViewModel);
        return View(updateRentalViewModel);
    }

    /*--------------- Deletes order in Order/Table---------------------*/
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DeleteRental(int id)
    {
        // Gets the ID of the rental the user wants to delete and goes to confirm deletion page
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental == null)
        {
            return NotFound();
        }
        if (rental == null)
        {
            _logger.LogError("[RentalController] Rental list not found for the RentalId {RentalId:0000}", id);
            return BadRequest("Rental not found for the RentalId");
        }

        return View(rental);

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteRentalConfirmed(int id)
    {
        // Gets the rental ID
        var rental = await _rentalRepository.GetRentalById(id);
        if (rental == null)
        {
            return NotFound();
        }

        // Deletes rental
        bool returnOk = await _rentalRepository.DeleteRental(id);
        if (!returnOk)
        {
            _logger.LogError("[RentalController] Rental deletion failed for the RentalId {RentalId:0000}", id);
            return BadRequest("Rental deletion failed");
        }

        await _rentalRepository.DeleteRental(id);
        return RedirectToAction("Table"); 

    }
}