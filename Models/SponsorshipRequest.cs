using System.ComponentModel.DataAnnotations;

namespace SponsorshipWorkflow.Api.Models
{

    public class SponsorshipRequest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string RequestTitle { get; set; }

        public Guid RequestorId { get; set; }

        public string Department { get; set; }

        public string SponsorshipType { get; set; }

        public string EventName { get; set; }

        public DateTime? EventDate { get; set; }

        public decimal RequestedAmount { get; set; }

        public string Purpose { get; set; }

        public string Status { get; set; }

        public string ManagerRemarks { get; set; }

        public string FinanceRemarks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
