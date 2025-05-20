using System.ComponentModel.DataAnnotations;
using CarRentalModels.Enums;

namespace CarRentalModels.Models;

public class User
{

    public User()
    {
        Rents = new List<Rent>();
    }
    public int UserId { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public DateTime DateOfBirth { get; set; }
    [Required] public string Address { get; set; }
    public string Address2 { get; set; }
    [Required] public string City { get; set; }
    [Required] public string County { get; set; }
    [Required] public string LicenseNumber { get; set; }
    [Required] public UserRole Role { get; set; } = UserRole.Customer;
    public List<Rent> Rents { get; set; }
}