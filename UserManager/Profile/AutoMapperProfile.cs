using AutoMapper;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser,UserDTO>().ReverseMap();
    }
}