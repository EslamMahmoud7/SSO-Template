using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using SSO_Template.DTOs.Models;
using SSO_Template.Interfaces;
using SSO_Template.Models.DTOs;
using SSO_Template.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SSO_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager1;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public TokenController(UserManager<AppUser> userManager, IConfiguration configuration, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager1 = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO logdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager1.Users.FirstOrDefaultAsync(u => u.UserName == logdto.Username);
            if (user == null)
                return Unauthorized("Invalid Username Or Password");
            var pass = await _signInManager.CheckPasswordSignInAsync(user, logdto.Password, false);
            if (!pass.Succeeded)
                return Unauthorized("Invalid Username Or Password");
            return Ok(_tokenService.CreateToken(user));
        }
        [HttpPost("registration")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterDTO register)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser = new AppUser()
                {
                    UserName = register.UserName,
                    Email = register.Email,
                };
                var createUser = await _userManager1.CreateAsync(appUser, register.Password);
                if (createUser.Succeeded)
                {
                    var roleResults = await _userManager1.AddToRoleAsync(appUser, "User");
                    if (roleResults.Succeeded)
                    {
                        return Ok(_tokenService.CreateToken(appUser));
                    }
                    else
                    {
                        return StatusCode(500, roleResults.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = e.Message });
            }
        }
    }
}
