using AutoMapper;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser,UserDTO>().ReverseMap();

        CreateMap<ApplicationRole,RoleDTO>()
                .ForMember(des=>des.Name,s=>s.MapFrom(src=>src.Name)).ReverseMap();

        CreateMap<ApplicationUser,UserRoleDTO>()
                .ForMember(des=>des.Roles,s=>s.MapFrom(src=>src.UserRoles.Select(ur=>ur.Role).ToList()));
    }
}