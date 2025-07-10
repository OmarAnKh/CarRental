using System.ComponentModel.DataAnnotations;
using CarRentalModels.Enums;

namespace CarRentalModels.Models;

public class Car
{
    public Car()
    {
        Rents = new List<Rent>();
    }
    public int CarId { get; set; }
    [Required] public string PlateNumber { get; set; }
    [Required] public string Model { get; set; }
    [Required] public string Color { get; set; }
    public CarAvailability Availability { get; set; }
    public List<Rent> Rents { get; set; }
}