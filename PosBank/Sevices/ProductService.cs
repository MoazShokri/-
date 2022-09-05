using Microsoft.EntityFrameworkCore;
using PosBank.Models;

namespace PosBank.Services
{
    public class ProductService:IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> Add(Product product)
        {
            await _context.AddAsync(product);
            _context.SaveChanges();
            return product;
        }
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(T => T.Id == id);
        }
        public Product Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
        public Product Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return (product);

        }
    }
}
