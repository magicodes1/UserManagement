using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Interfaces.Services;
using UserManagement.UserManager.Interfaces.UnitOfWork;
using UserManagement.UserManager.Models.DataResponse;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Services;

public class UserService : IUserService
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly ITokenService _token;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService token)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _token = token;
    }

    public async Task<DataResponse> List()
    {
        var users = await _unitOfWork.UserRepository.List();

        if (users == null) throw new NotFoundException("Users are not found.");

        var userDTOs = users.Select(p => _mapper.Map<UserRoleDTO>(p));

        return new DataResponse(true, userDTOs, null!);
    }

    public async Task Update(string id, UpdateUserDTO updateUserDTO, string token)
    {
        try
        {
            var claims = _token.getCLaim(token);

            var result = ShouldBeUpdating(id, claims!.ToList());

            if (!result) throw new ForbiddenException("You cannot do this with your current role");

            await _unitOfWork.UserRepository.Update(id, updateUserDTO);

            await _unitOfWork.SaveChangeAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {

            throw ex;
        }
    }


    private bool ShouldBeUpdating(string id, List<Claim> claims)
    {
        string userId = "";
        List<string> roles = new List<string>();
        foreach (var item in claims)
        {
            if (item.Type == ClaimTypes.NameIdentifier)
            {
                userId = item.Value;
            }

            if (item.Type == ClaimTypes.Role)
            {
                roles.Add(item.Value);
            }
        }

        var role = roles.FirstOrDefault(p => p == "ADMIN");

        if (userId != id && role == null) return false;
        return true;
    }
}