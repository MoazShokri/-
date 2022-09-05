using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosBank.Dto;
using PosBank.Models;
using PosBank.Services;

namespace PosBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet]
        public async Task<IActionResult>GetAllMenu()
        {
            var menu = await _menuService.GetAll();
            return Ok(menu);
        }
        [HttpPost]
        public async Task<IActionResult>AddItem([FromForm]MenuDto menuDto)
        {
            if (menuDto.Picture == null)
                return BadRequest("Picture is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(menuDto.Picture.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (menuDto.Picture.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for picture is 1MB!");
            
            using var dataStream = new MemoryStream();

            await menuDto.Picture.CopyToAsync(dataStream);

            var menu = new Menu
            {
                Name = menuDto.Name,
                Price = menuDto.Price,
                Size = menuDto.Size,
                Picture = dataStream.ToArray()


        };
            await _menuService.Add(menu);
            return Ok(menu);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id ,[FromForm]MenuDto menuDto)
        {
            var menu = await _menuService.GetById(id);
            if (menu == null)
                return NotFound($"this item is not exit in Menu {id} ");
            if (!_allowedExtenstions.Contains(Path.GetExtension(menuDto.Picture.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (menuDto.Picture.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for picture is 1MB!");

            using var dataStream = new MemoryStream();

            await menuDto.Picture.CopyToAsync(dataStream);

            {
                menu.Name = menuDto.Name;
                menu.Price = menuDto.Price;
                menu.Size = menuDto.Size;
                menu.Picture = dataStream.ToArray();

                _menuService.Update(menu);
                return Ok(menu);
            }
           
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteItem(int id )
        {
            var item = await _menuService.GetById(id);
            if (item == null)
                return NotFound("this item is not exit in menu");
            _menuService.Delete(item);
            return Ok(item);
        }

    }
}
