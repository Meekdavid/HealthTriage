using Persistence.Concrete;
using Persistence.Enums;

public class ConsultancyChat : HealthTriageEntity
{
    public ConsultancyChat()
    {
        ConsultancyChatId = string.Empty;
        Text = string.Empty;
        UserName = string.Empty;

        Status = Status.Active;
    }

    public string? ConsultancyChatId { get; set; }
    public string? Text { get; set; }
    public string? UserName { get; set; }
}
