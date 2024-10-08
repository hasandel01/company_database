﻿using System.Text.Json.Serialization;

namespace CompanyDatabase.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReportedDate { get; set; } = DateTime.UtcNow;
        public DateTime ResolvedDate {  get; set; } = DateTime.UtcNow;
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
