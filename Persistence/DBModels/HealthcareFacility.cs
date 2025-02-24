using Persistence.Concrete;
using Persistence.Enums;

public class HealthcareFacility : HealthTriageEntity
{
    public HealthcareFacility()
    {
        Status = Status.Active;
        HealthcareFacilityId = string.Empty;
        FacilityName = string.Empty;
        FacilityType = string.Empty;
        Address = string.Empty;
        City = string.Empty;
        State = string.Empty;
        ZipCode = string.Empty;
        PhoneNumber = string.Empty;
        Email = string.Empty;
        WebsiteUrl = string.Empty;
        OperatingHours = string.Empty;
        ServicesOffered = string.Empty;
        Specialties = string.Empty;
        LicenseNumber = string.Empty;
        AccreditationStatus = string.Empty;
        FacilityRating = 0.0f;
        EmergencyServicesAvailable = false;
        InsuranceAccepted = string.Empty;
        NumberOfBeds = null;
        StaffCount = 0;
        LastUpdated = DateTime.MinValue;
    }

    public string? HealthcareFacilityId { get; set; }
    public string? FacilityName { get; set; }
    public string? FacilityType { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? OperatingHours { get; set; }
    public string? ServicesOffered { get; set; }
    public string? Specialties { get; set; }
    public string? LicenseNumber { get; set; }
    public string? AccreditationStatus { get; set; }
    public float? FacilityRating { get; set; }
    public bool? EmergencyServicesAvailable { get; set; }
    public string? InsuranceAccepted { get; set; }
    public int? NumberOfBeds { get; set; }
    public int? StaffCount { get; set; }
    public DateTime? LastUpdated { get; set; }
}
