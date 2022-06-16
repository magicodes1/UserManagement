using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

    public AccountService(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
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