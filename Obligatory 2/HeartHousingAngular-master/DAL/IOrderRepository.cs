using HeartHousingAngular.Models;
using System.Runtime.InteropServices;

namespace HeartHousingAngular.DAL
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>?> GetAllOrders();
        Task<Order?> GetOrderById(int id);
        Task<bool> CreateOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
        //Task<IEnumerable<Order>?> GetAllOrdersByUserId(string id);


    }
}
