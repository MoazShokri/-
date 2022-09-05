namespace PosBank.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public byte[] Picture { get; set; }
        
    }
}
