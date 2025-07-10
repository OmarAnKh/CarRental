using AutoMapper;
using CarRental.Models.CarDto;
using CarRentalModels.Models;

namespace CarRental.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDto>();
        CreateMap<CarCreationDto, Car>();
    }

}