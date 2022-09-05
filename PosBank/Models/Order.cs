namespace PosBank.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public long CustomerPhone { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
