using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyDatabase.Models
{
    public class Stock
    {
    
        [Required]
        public int Id { get; set; }

        [Key]
        public int ProductId { get; set; }

        [Key]
        public int StoreId { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Navigation property to product
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        // Navigation property to store
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        
    }
}
