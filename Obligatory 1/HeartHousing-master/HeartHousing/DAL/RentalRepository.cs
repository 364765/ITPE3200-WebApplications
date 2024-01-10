using HeartHousing.Models;
using Microsoft.EntityFrameworkCore;

namespace HeartHousing.DAL;

public class RentalRepository : IRentalRepository
{
    private readonly RentalDbContext _db;
    private readonly ILogger<RentalRepository> _logger;


    public RentalRepository(RentalDbContext db, ILogger<RentalRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<Rental>?> GetAllRentals()
    {
        try
        {
            return await _db.Rentals.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("[RentalRepository] orders ToListAsync() failed when GetAllRentals(), error message: {e}", e.Message);
            return null;
        }
    }

    public async Task<Rental?> GetRentalById(int id)
    {
        try
        {
            return await _db.Rentals.FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError("[RentalRepository] order FindAsync(id) failed when GetRentalById for RentalId {RentalId:0000}, error message: {e}", id, e.Message);
            return null;
        }

    }

    public async Task<bool> CreateRental(Rental rental)
    {
        try
        {
            _db.Rentals.Add(rental);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RentalRepository] rental creation failed for rental {@rental}, error message: {e}", rental, e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateRental(Rental rental)
    {
        try
        {
            _db.Rentals.Update(rental);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RentalRepository] rental SaveChangesAsync() failed when updating RentalId {@RentalId:0000}, error message: {e}", rental, e.Message);
            return false;
        }

    }

    public async Task<bool> DeleteRental(int id)
    {
        try
        {
            var rental = await _db.Rentals.FindAsync(id);
            if (rental == null)
            {
                _logger.LogError("[RentalRepository] rental not found for the RentalId {RentalId:0000}", id);
                return false;
            }
            _db.Rentals.Remove(rental);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RentalRepository] rental deletion failed for the RentalId {RentalId:0000}, error message: {e}", id, e.Message);
            return false;
        }

    }
    
    public async Task<IEnumerable<Rental>?> GetAllRentalsByUserId(string userId)
    {
        try
        {
            //Filters rentals that has the matching userId of the currently logged in UserID and makes it to list
            return await _db.Rentals
                .Where(rental => rental.UserID == userId)
                .ToListAsync();

        }
        catch (Exception e)
        {
            _logger.LogError("[RentalRepository] rentals ToListAsync() failed when GetAllRentalsByUserId(), error message: {e}", e.Message);
            return null;
        }
    }
}