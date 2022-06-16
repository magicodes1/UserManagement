using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Interfaces.Services;
using UserManagement.UserManager.Models.DataResponse;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IMapper _mapper;

    private readonly ITokenService _token;

    public AccountService(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IMapper mapper,
                            ITokenService token)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _token = token;
    }

    public async Task<DataResponse> Signin(SigninDTO signinDTO)
    {
        var user = await _userManager.Users.Where(u => u.UserName == signinDTO.UserName)
                       .Include(u => u.UserRoles)
                       .ThenInclude(r => r.Role)
                       .SingleOrDefaultAsync();

        if (user == null) throw new NotFoundException("User is not found");

        var isCorrect = await _userManager.CheckPasswordAsync(user, signinDTO.Password);

        if (!isCorrect) throw new BadRequestException("Password is wrong for this user.");

        var userRoleDTO = _mapper.Map<UserRoleDTO>(user);

        var roles = new List<string>();
        foreach (var role in userRoleDTO.Roles)
        {
            roles.Add(role.Name);
        }

        var token = _token.tokenGeneration(userRoleDTO.UserName, userRoleDTO.Id, roles);

        return new DataResponse(true, token, null!);

    }

    public async Task<DataResponse> Signup(SignupDTO signupDTO)
    {
        var user = new ApplicationUser
        {
            UserName = signupDTO.UserName,
            PhoneNumber = signupDTO.PhoneNumber,
            FullName = signupDTO.FullName
        };

        var result = await _userManager.CreateAsync(user, signupDTO.Password);



        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                throw new BadRequestException($"{error.Description}");
            }

        }


        var userDTO = _mapper.Map<UserDTO>(user);

        return new DataResponse(true, userDTO, null!);
    }
}