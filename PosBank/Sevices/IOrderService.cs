using PosBank.Models;

namespace PosBank.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> Add(Order order);
        Task<Order> GetById(int id);
        Order Update(Order order);
        Order Delete(Order order);
    }
}
