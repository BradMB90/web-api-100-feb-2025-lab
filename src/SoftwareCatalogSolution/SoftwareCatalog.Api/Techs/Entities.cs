namespace SoftwareCatalog.Api.Techs;

public class TechEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Sub { get; set; } = string.Empty;
    public string AddedBySub { get; set; } = string.Empty;
}
