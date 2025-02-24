using Persistence.Concrete;
using Persistence.DBModels;
using Persistence.Enums;

public class ConsultationHistory : HealthTriageEntity
{
    public ConsultationHistory()
    {
        ConsultationHistoryId = string.Empty;
        UserId = string.Empty;
        PractitionerId = string.Empty;

        Status = Status.Active;
        ConsultancyChats = new HashSet<ConsultancyChat>();
    }

    public string? ConsultationHistoryId { get; set; }
    public string? UserId { get; set; }
    public string? PractitionerId { get; set; }

    public virtual AppUser? AppUser { get; set; }
    public virtual Practitioner? Practitioner { get; set; }
    public virtual ICollection<ConsultancyChat> ConsultancyChats { get; set; }
}
