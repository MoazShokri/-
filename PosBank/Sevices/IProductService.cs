using PosBank.Models;

namespace PosBank.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Add(Product product);
        Task<Product> GetById(int id);
        Product Update(Product product);
        Product Delete(Product product);
    }
}
