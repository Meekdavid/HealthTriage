using Persistence.Concrete;
using Persistence.Enums;

public class FAQ : HealthTriageEntity
{
    public FAQ()
    {
        Status = Status.Active;
        FAQId = 0;
        Question = string.Empty;
        Answer = string.Empty;
        AddedBy = string.Empty;
    }

    public int? FAQId { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public string? AddedBy { get; set; }
}
