using PosBank.Models;

namespace PosBank.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAll();
        Task<Menu> GetById(int id);
        Task<Menu> Add(Menu menu);
        Menu Update(Menu menu);
        Menu Delete(Menu menu);
    }
}
