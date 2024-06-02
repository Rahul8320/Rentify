using Rentify.Application.Models.Responses;
using Rentify.Domain.Entities;

namespace Rentify.Application.Mapping;

public static class Mapper
{
    public static PropertyResponse ToModel(this Property property, UserResponse? userResponse = null)
    {
        return new PropertyResponse()
        {
            Id = property.Id,
            Place = property.Place,
            Description = property.Description,
            LastUpdated = property.LastUpdated,
            Owner = property.Owner,
            OwnerDetails = userResponse,
            Likes = property.Likes,
            NearbyCollege = property.NearbyCollege,
            NearbyHospital = property.NearbyHospital,
            NearbySchool = property.NearbySchool,
            NoOfBathroom = property.NoOfBathroom,
            NoOfBedroom = property.NoOfBedroom,
            Price = property.Price,
            SizeinSqft = property.SizeinSqft
        };
    }

    public static UserResponse ToModel(this ApplicationUser applicationUser)
    {
        return new UserResponse()
        {
            Id = applicationUser.Id,
            Email = applicationUser.Email ?? "",
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Phone = applicationUser.PhoneNumber ?? "",
        };
    }
}