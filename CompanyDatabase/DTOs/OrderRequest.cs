using CompanyDatabase.DTOs;

public class OrderRequest
{
    public int BranchId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Status { get; set; }
    public int CustomerId { get; set; }
    public List<ProductQuantity> ProductQuantities { get; set; } // Adjusted field
}
