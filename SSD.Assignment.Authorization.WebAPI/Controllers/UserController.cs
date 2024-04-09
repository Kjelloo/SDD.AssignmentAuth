using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSD.Assignment.Authorization.WebAPI.Controllers.Dtos;
using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IAuthService authService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_userService.GetAll());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [AllowAnonymous] 
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginUserDto loginUser)
    {
        try
        {
            var user = _userService.GetByUsername(loginUser.Username);
                
            if (user == null)
                return Unauthorized();
                
            var isValid = _authService.VerifyPassword(loginUser.Password, user);
                
            if (!isValid)
                return Unauthorized();
                
            var token = _authService.GenerateToken(user);
                
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not login");
            throw;
        }
    }
}