public class Country
{
    public Country()
    {
        CountryName = string.Empty;
        ISOCode2 = string.Empty;
        ISOCode3 = string.Empty;
        PhoneCode = string.Empty;
        Flag = string.Empty;
    }

    public string? CountryName { get; set; }
    public string? ISOCode2 { get; set; }
    public string? ISOCode3 { get; set; }
    public string? PhoneCode { get; set; }
    public string? Flag { get; set; }
}
