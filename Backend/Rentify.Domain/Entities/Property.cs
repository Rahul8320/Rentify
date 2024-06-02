namespace Rentify.Domain.Entities;

public class Property : BaseEntity
{
    public Guid Owner { get; set; }
    public string Place { get; set; } = string.Empty;
    public int NoOfBedroom { get; set; }
    public int NoOfBathroom { get; set; }
    public int SizeinSqft { get; set; }
    public bool NearbySchool { get; set; }
    public bool NearbyCollege { get; set; }
    public bool NearbyHospital { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Guid> Likes { get; set; } = [];
}
