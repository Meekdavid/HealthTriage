using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<string>
{
    public AppUser()
    {
        CreatedDate = DateTime.UtcNow;
        Id = Ulid.NewUlid().ToString();

        // Initialize all string properties to an empty string
        FullName = string.Empty;
        ProfilePicture = string.Empty;
        DOB = string.Empty;
        Gender = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        ZipCode = string.Empty;
        BloodGroup = string.Empty;
        Height = string.Empty;
        Weight = string.Empty;
        EmergencyContact = string.Empty;
        RefreshToken = string.Empty;
    }

    public string? FullName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? DOB { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ZipCode { get; set; }
    public string? BloodGroup { get; set; }
    public string? Height { get; set; }
    public string? Weight { get; set; }
    public string? EmergencyContact { get; set; }
    public string? RefreshToken { get; set; }
    public string? Role { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    public DateTime? LastActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
