using HeartHousing.DAL;
using HeartHousing.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HeartHousing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRentalRepository _rentalRepository;

        public HomeController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        // Displays RentalCards on index page
        public async Task<IActionResult> IndexAsync()
        {
            var rentals = await _rentalRepository.GetAllRentals();
            var rentalListViewModel = new RentalListViewModel(rentals, "Grid");
            return View(rentalListViewModel);
        }
    }
}

