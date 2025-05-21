using AutoMapper;
using CarRental.Models.UserDto;
using CarRentalModels.Models;

namespace CarRental.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserCreationDto, User>();
        CreateMap<User, UserCreationDto>();
        CreateMap<User, UserUpdateRoleDto>();
        CreateMap<UserUpdateRoleDto, User>();

    }
}