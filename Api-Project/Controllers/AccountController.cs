using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api_Project.DTOs.AppUser;
using Api_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> manager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<AppUser> manager , SignInManager<AppUser> signInManager, IConfiguration config)
        {
            this.manager = manager;
            this.signInManager = signInManager;
            this.config = config;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto dto )
        {
            if(!ModelState.IsValid) 
                return BadRequest("Data Null");

            if (dto == null)
                return BadRequest("Data Null");
            var email = await manager.FindByEmailAsync(dto.Email);
            if (email != null)
                return BadRequest("Email is use");

            var appUser = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Address = dto.Address,
                //UserType = dto.UserType,
            };
            IdentityResult result = await manager.CreateAsync(appUser,dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Created();   
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data Null");

            var user = await manager.FindByNameAsync(dto.UserName)
                ?? await manager.FindByEmailAsync(dto.UserName);
            if (user == null)
                return BadRequest("User name Or Password Wrong");

            //bool result = await manager.CheckPasswordAsync(user,dto.Password);
            //if(result == false)
            //    return BadRequest("User name Or Password Wrong");

            var result = await signInManager.PasswordSignInAsync
                (user, dto.Password, isPersistent: false, lockoutOnFailure: true);
            if (!result.Succeeded)
                return Unauthorized("User name Or Password Wrong");
            //Generate token
            var token = GenerateToken(user);

            return Ok(token);   
        }

        private string GenerateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"]));

            SigningCredentials signingCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["jwt:issuer"],
                audience: config["jwt:audience"],
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials : signingCredentials,
                claims : claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
