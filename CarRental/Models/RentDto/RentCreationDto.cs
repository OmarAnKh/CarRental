namespace CarRental.Models.RentDto;

public class RentCreationDto
{
    public int CarId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
}