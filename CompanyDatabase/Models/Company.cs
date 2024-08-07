﻿namespace CompanyDatabase.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
