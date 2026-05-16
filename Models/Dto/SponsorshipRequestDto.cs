namespace SponsorshipWorkflow.Api.Models.Dto
{
    public class SponsorshipRequestDto
    {
        public string RequestTitle { get; set; }
        public string RequestorId { get; set; } = string.Empty;
        public string Department { get; set; }
        public string SponsorshipType { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal RequestedAmount { get; set; }
        public string Purpose { get; set; }
        public string? ExpectedBusinessBenefit { get; set; }
        public string? Remarks { get; set; }
    }
}
