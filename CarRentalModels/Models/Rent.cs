using System.ComponentModel.DataAnnotations;

namespace CarRentalModels.Models;

public class Rent
{
    public int RentId { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    [Required] public DateTime RentDate { get; set; }
    [Required] public DateTime ReturnDate { get; set; }
}