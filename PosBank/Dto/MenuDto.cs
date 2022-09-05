namespace PosBank.Dto
{
    public class MenuDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
