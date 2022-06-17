using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Interfaces.Services;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AccountController> _logger;
    public UserController(IUserService userService, ILogger<AccountController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "USER,ADMIN")]
    public async Task<IActionResult> getAllUsers()
    {
        var users = await _userService.List();
        return Ok(users);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "USER,ADMIN")]
    public async Task<IActionResult> updateUser(string id, UpdateUserDTO updateUserDTO)
    {
        if (updateUserDTO == null) throw new BadRequestException("payload is null");

        if (!ModelState.IsValid) throw new BadRequestException("Model is invalid");

        string auth = Request.Headers.Authorization;
        var start = auth.IndexOf(' ');
        var token = auth.Substring(start + 1);

        await _userService.Update(id, updateUserDTO, token);

        return NoContent();
    }
}