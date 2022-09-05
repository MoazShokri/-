namespace PosBank.Dto
{
    public class OrderDto
    {
        public string CustomerName { get; set; }
        public long CustomerPhone { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
    }
}
