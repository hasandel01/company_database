﻿namespace CompanyDatabase.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
