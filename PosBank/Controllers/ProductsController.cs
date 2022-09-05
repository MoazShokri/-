using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosBank.Dto;
using PosBank.Models;
using PosBank.Services;

namespace PosBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _productService.GetAll();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            if (productDto.Picture == null)
                return BadRequest("Picture is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(productDto.Picture.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (productDto.Picture.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for picture is 1MB!");

            using var dataStream = new MemoryStream();

            await productDto.Picture.CopyToAsync(dataStream);

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Size = productDto.Size,
                Picture = dataStream.ToArray(),
                

            };
            await _productService.Add(product);
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound($"this item is not exit in Menu {id} ");
            if (!_allowedExtenstions.Contains(Path.GetExtension(productDto.Picture.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (productDto.Picture.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for picture is 1MB!");

            using var dataStream = new MemoryStream();

            await productDto.Picture.CopyToAsync(dataStream);

            {
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Size = productDto.Size;
                product.Picture = dataStream.ToArray();

                _productService.Update(product);
                return Ok(product);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound("this product is not exit in Products");
            _productService.Delete(product);
            return Ok(product);
        }




    }
}
