


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


    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService account, ILogger<AccountController> logger)
    {
        _account = account;
        _logger = logger;
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
}