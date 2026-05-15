using System.ComponentModel.DataAnnotations;

public class RequestWorkflowHistory
{
    [Key]
    public int Id { get; set; }

    public Guid RequestId { get; set; }

    public Guid ActionByUserId { get; set; }

    public string OldStatus { get; set; }

    public string NewStatus { get; set; }

    public string Remarks { get; set; }

    public DateTime ActionDate { get; set; } = DateTime.UtcNow;
}