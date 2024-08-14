namespace CompanyDatabase.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

    }
}
