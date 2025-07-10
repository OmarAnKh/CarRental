namespace CarRental.Models.RentDto;

public class RentDto
{
    public int RentId { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
}