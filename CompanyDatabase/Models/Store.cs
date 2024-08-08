using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyDatabase.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public int? ParentStoreId { get; set; }
        // Navigation property to child stores (shelves within this store)
        [ForeignKey("ParentStoreId")]
        public virtual Store? ParentStore { get; set; }
        public ICollection<Store> ChildStores { get; set; } = new List<Store>();
        // Navigation property to stock records
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
