using System.ComponentModel.DataAnnotations;

namespace SponsorshipWorkflow.Api.Models
{
    public class SponsorshipRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string RequestTitle { get; set; } = string.Empty;

        public string RequestorId { get; set; }

        public string Department { get; set; } = string.Empty;

        public string SponsorshipType { get; set; } = string.Empty;

        public string EventName { get; set; } = string.Empty;

        public DateTime? EventDate { get; set; }

        public decimal RequestedAmount { get; set; } = 0;

        public string Purpose { get; set; } = string.Empty;

        public string Status { get; set; } = "Draft";

        public string ManagerRemarks { get; set; } = string.Empty;

        public string FinanceRemarks { get; set; } = string.Empty;
        public string RequestorRemarks { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }


        //public string Remarks { get; set; } = string.Empty;
    }
}