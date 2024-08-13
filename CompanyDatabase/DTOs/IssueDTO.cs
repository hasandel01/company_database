namespace CompanyDatabase.DTOs
{
    public class IssueDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReportedDate { get; set; }
        // Exclude fields like Id, ProductId, and ResolvedDate
    }

}
