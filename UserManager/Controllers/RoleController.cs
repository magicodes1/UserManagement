using Microsoft.AspNetCore.Mvc;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Interfaces.Services;

namespace UserManagement.UserManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _role;

    public RoleController(IRoleService role)
    {
        _role = role;
    }
    [HttpPost]
    public async Task<IActionResult> AddRoleToUser(AddRoleDTO addRoleDTO)
    {
        if (addRoleDTO == null) throw new BadRequestException("payload is null");

        if (!ModelState.IsValid) throw new BadRequestException("Model is invalid");

        var result = await _role.AddRole(addRoleDTO);

        return Ok(result);
    }

}