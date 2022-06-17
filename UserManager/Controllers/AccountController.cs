


using Microsoft.AspNetCore.Mvc;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Interfaces.Services;

namespace UserManagement.UserManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _account;
    public AccountController(IAccountService account)
    {
        _account = account;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(SignupDTO signupDTO)
    {
        if (signupDTO == null) throw new BadRequestException("payload is null");

        if (!ModelState.IsValid) throw new BadRequestException("Model is invalid");

        var result = await _account.Signup(signupDTO);

        return Ok(result);
    }

    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> Signin(SigninDTO signinDTO)
    {
        if (signinDTO == null) throw new BadRequestException("payload is null");

        if (!ModelState.IsValid) throw new BadRequestException("Model is invalid");

        var result = await _account.Signin(signinDTO);

        return Ok(result);
    }
}