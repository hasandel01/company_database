using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyDatabase.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
 
        public ICollection<OrderProduct> OrderProducts { get; set; }

        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    }
}
