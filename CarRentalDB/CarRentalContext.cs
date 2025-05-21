using CarRentalModels.Enums;
using CarRentalModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRentalDB;

public class CarRentalContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Rent> Rents { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DotNetEnv.Env.Load();

        optionsBuilder
            .UseSqlServer(Environment.GetEnvironmentVariable("SQLSERVERCONNECTIONSTRING"))
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "hashedpwd1",
                PhoneNumber = "1234567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "123 Main St",
                Address2 = "Apt 1",
                City = "New York",
                County = "NY",
                LicenseNumber = "NY123456",
                Role = UserRole.Customer
            },
            new User
            {
                UserId = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Password = "hashedpwd2",
                PhoneNumber = "0987654321",
                DateOfBirth = new DateTime(1985, 5, 23),
                Address = "456 Broadway",
                Address2 = "Nablus",
                City = "Chicago",
                County = "IL",
                LicenseNumber = "IL789456",
                Role = UserRole.Customer
            },
            new User
            {
                UserId = 3,
                FirstName = "Ali",
                LastName = "Khan",
                Email = "ali.khan@example.com",
                Password = "hashedpwd3",
                PhoneNumber = "5551112233",
                DateOfBirth = new DateTime(1992, 9, 12),
                Address = "789 Sunset Blvd",
                Address2 = "Ramallah",
                City = "Los Angeles",
                County = "CA",
                LicenseNumber = "CA456789",
                Role = UserRole.Admin
            },
            new User
            {
                UserId = 4,
                FirstName = "Sara",
                LastName = "Lee",
                Email = "sara.lee@example.com",
                Password = "hashedpwd4",
                PhoneNumber = "4445556666",
                DateOfBirth = new DateTime(1995, 3, 15),
                Address = "321 Palm Ave",
                Address2 = "Suite 10",
                City = "Miami",
                County = "FL",
                LicenseNumber = "FL112233",
                Role = UserRole.Customer
            },
            new User
            {
                UserId = 5,
                FirstName = "Mohammed",
                LastName = "Zayed",
                Email = "mohammed.zayed@example.com",
                Password = "hashedpwd5",
                PhoneNumber = "3334445555",
                DateOfBirth = new DateTime(1988, 7, 7),
                Address = "101 Desert Rd",
                Address2 = "IDk",
                City = "Phoenix",
                County = "AZ",
                LicenseNumber = "AZ778899",
                Role = UserRole.Customer
            }
        );

        modelBuilder.Entity<Car>().HasData(
            new Car { CarId = 1, PlateNumber = "ABC123", Model = "Toyota Corolla", Color = "Red", Availability = CarAvailability.Available },
            new Car { CarId = 2, PlateNumber = "XYZ789", Model = "Honda Civic", Color = "Blue", Availability = CarAvailability.Unavailable },
            new Car { CarId = 3, PlateNumber = "LMN456", Model = "Ford Mustang", Color = "Black", Availability = CarAvailability.Available },
            new Car { CarId = 4, PlateNumber = "JKL321", Model = "Chevrolet Malibu", Color = "White", Availability = CarAvailability.Available },
            new Car { CarId = 5, PlateNumber = "DEF654", Model = "Nissan Altima", Color = "Silver", Availability = CarAvailability.Unavailable }
        );

        modelBuilder.Entity<Rent>().HasData(
            new Rent { RentId = 1, CarId = 1, UserId = 1, RentDate = new DateTime(2024, 6, 1), ReturnDate = new DateTime(2024, 6, 5) },
            new Rent { RentId = 2, CarId = 2, UserId = 2, RentDate = new DateTime(2024, 6, 3), ReturnDate = new DateTime(2024, 6, 7) },
            new Rent { RentId = 3, CarId = 3, UserId = 3, RentDate = new DateTime(2024, 6, 4), ReturnDate = new DateTime(2024, 6, 6) },
            new Rent { RentId = 4, CarId = 4, UserId = 4, RentDate = new DateTime(2024, 6, 2), ReturnDate = new DateTime(2024, 6, 8) },
            new Rent { RentId = 5, CarId = 5, UserId = 5, RentDate = new DateTime(2024, 6, 5), ReturnDate = new DateTime(2024, 6, 10) }
        );
    }
}