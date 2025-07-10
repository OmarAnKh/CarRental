using CarRentalModels.Enums;

namespace CarRental.Models.UserDto;

public class UserWithRentsDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string LicenseNumber { get; set; }
    public UserRole Role { get; set; } = UserRole.Customer;
    public List<RentDto.RentDto> Rents { get; set; }
}