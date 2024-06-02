using System.ComponentModel.DataAnnotations;

namespace Rentify.Application.Models.Requests;

public class CreatePropertyRequest
{
    [Required, MinLength(5), MaxLength(500)]
    public string Place { get; set; } = string.Empty;
    [Required, Range(1, 100)]
    public int NoOfBedroom { get; set; }
    [Required, Range(1, 100)]
    public int NoOfBathroom { get; set; }
    [Required, Range(100, 10000)]
    public int SizeinSqft { get; set; }
    [Required]
    public bool NearbySchool { get; set; }
    [Required]
    public bool NearbyCollege { get; set; }
    [Required]
    public bool NearbyHospital { get; set; }
    [Required, MinLength(10), MaxLength(5000)]
    public string Description { get; set; } = string.Empty;
    [Required, Range(1000, double.MaxValue)]
    public decimal Price { get; set; }
}
