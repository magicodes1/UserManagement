using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Interfaces.Services;
using UserManagement.UserManager.Models.DataResponse;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IMapper _mapper;

    public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<DataResponse> AddRole(AddRoleDTO addRoleDTO)
    {
        var roles = addRoleDTO.Roles;

        ApplicationUser user = null!;

        foreach (var role in roles)
        {
           user = await AddRoleToUser(addRoleDTO.Id, role);
        }

        var userRoleDTO = _mapper.Map<UserRoleDTO>(user);

        return new DataResponse(true,userRoleDTO,null!);
    }

    private async Task<ApplicationUser> AddRoleToUser(string id, string role)
    {
        //Create new role if it isn't existense.
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new ApplicationRole {Name=role,NormalizedName=role});
        }

        var user = await _userManager.FindByIdAsync(id);

        if(user==null) throw new NotFoundException($"User {id} isn't found");

        var result = await _userManager.AddToRoleAsync(user,role);

        if(!result.Succeeded) 
        {
            foreach (var error in result.Errors)
            {
                throw new BadRequestException($"{error.Description}");
            }
        }

        return user;
    }
}