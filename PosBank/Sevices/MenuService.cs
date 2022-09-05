using Microsoft.EntityFrameworkCore;
using PosBank.Models;

namespace PosBank.Services
{
    public class MenuService:IMenuService
    {
        private readonly ApplicationDbContext _context;
        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Menu>>GetAll()
        {
            return await _context.Menus.ToListAsync();
        }
        public async Task<Menu> Add(Menu menu)
        {
            await _context.AddAsync(menu);
            _context.SaveChanges();
            return menu;
        }
        public async Task<Menu> GetById(int id)
        {
            return await _context.Menus.SingleOrDefaultAsync(T => T.Id == id);
        }
        public Menu Update(Menu menu)
        {
            _context.Update(menu);
            _context.SaveChanges();
            return menu;
        }
        public Menu Delete(Menu menu)
        {
            _context.Remove(menu);
            _context.SaveChanges();
            return (menu);
        }
    }
}
