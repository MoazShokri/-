using Microsoft.EntityFrameworkCore;
using PosBank.Models;

namespace PosBank.Services
{
    public class OrderService:IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.Include(o=>o.Product).ToListAsync();
        }
        public async Task<Order> Add(Order order)
        {
            await _context.AddAsync(order);
            _context.SaveChanges();
            return order;
        }
        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Include(o=>o.Product).SingleOrDefaultAsync(T => T.Id == id);
        }
        public Order Update(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }
        public Order Delete(Order order)
        {
            _context.Remove(order);
            _context.SaveChanges();
            return (order);
        }


    }
}
