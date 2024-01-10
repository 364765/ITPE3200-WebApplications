using HeartHousing.Models;

namespace HeartHousing.DAL
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentals();
        Task<Rental?> GetRentalById(int id);
        Task<bool> CreateRental(Rental rental);
        Task<bool> UpdateRental(Rental rental);
        Task<bool> DeleteRental(int id);
        Task<IEnumerable<Rental>?> GetAllRentalsByUserId(string id);

        //Task<bool> Rent(int id);
    }
}