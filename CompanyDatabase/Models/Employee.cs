using Newtonsoft.Json;

namespace CompanyDatabase.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
