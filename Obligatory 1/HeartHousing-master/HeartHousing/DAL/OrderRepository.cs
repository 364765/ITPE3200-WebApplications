using Microsoft.EntityFrameworkCore;
using HeartHousing.Models;

namespace HeartHousing.DAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RentalDbContext _db;

        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(RentalDbContext db, ILogger<OrderRepository> logger)
        {
            _db = db;
            _logger = logger;

        }

        public async Task<IEnumerable<Order>?> GetAllOrders()
        {
            try
            {
                return await _db.Orders.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] orders ToListAsync() failed when GetAllOrders(), error message: {e}", e.Message);
                return null;
            }
        }

        public async Task<Order?> GetOrderById(int id)
        {
            try
            {
                return await _db.Orders.FindAsync(id);

            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] order FindAsync(id) failed when GetOrderById for OrderId {OrderId:0000}, error message: {e}", id, e.Message);
                return null;
            }
        }

        public async Task<bool> CreateOrder(Order order)
        {
            try
            {
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] order creation failed for order {@order}, error message: {e}", order, e.Message);
                return false;

            }
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                _db.Orders.Update(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] order SaveChangesAsync() failed when updating the OrderId {OrderId:0000}, error message: {e}", order, e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var order = await _db.Orders.FindAsync(id);
                if (order == null)
                {
                    _logger.LogError("[OrderRepository] order not found for the OrderId {OrderId:0000}", id);
                    return false;
                }
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] order deletion failed for the OrderId {OrderId:0000}, error message: {e}", id, e.Message);
                return false;
            }
        }


        public async Task<IEnumerable<Order>?> GetAllOrdersByUserId(string userId)
        {
            try
            {
                //Filters orders that has the matching userId of the currently logged in UserID and makes it to list
                return await _db.Orders
           .Where(order => order.UserID == userId)
           .ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] orders ToListAsync() failed when GetAllOrdersByUserId(), error message: {e}", e.Message);
                return null;
            }
        }
    }
}
