using AutoMapper;
using CarRental.Models.RentDto;
using CarRentalModels.Models;

namespace CarRental.Profiles;

public class RentProfile : Profile
{
    public RentProfile()
    {
        CreateMap<Rent, RentDto>();
        CreateMap<RentDto, Rent>();
        CreateMap<RentCreationDto, Rent>();

    }
}