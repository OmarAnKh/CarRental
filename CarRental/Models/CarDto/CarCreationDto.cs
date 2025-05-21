using System.ComponentModel.DataAnnotations;

namespace CarRental.Models.CarDto;

public class CarCreationDto
{
    [Required] public string PlateNumber { get; set; }
    [Required] public string Model { get; set; }
    [Required] public string Color { get; set; }
}