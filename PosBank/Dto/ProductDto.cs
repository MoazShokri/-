namespace PosBank.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
