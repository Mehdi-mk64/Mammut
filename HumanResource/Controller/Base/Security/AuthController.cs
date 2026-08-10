using Common.Security;
using Entities.Basic.Security;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace HumanResource.Controller.Security
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController( UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
        }




        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return Unauthorized("Username or password is incorrect.");

            if (await _userManager.IsLockedOutAsync(user))
                return Unauthorized("User is disabled.");

            var passwordIsValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    model.Password);

            if (!passwordIsValid)
                return Unauthorized("Username or password is incorrect.");

            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateToken(user, roles);

            return Ok(new
            {
                Token = token,
                user.Id,
                user.UserName,
                user.PersonID,
                Roles = roles
            });
        }



        private string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var encryptionKey = Encoding.UTF8.GetBytes(_jwtSettings.EncryptionKey);

            var signingCredentials = new SigningCredentials( new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptingCredentials = new EncryptingCredentials(
                new SymmetricSecurityKey(encryptionKey),
                SecurityAlgorithms.Aes128KW,
                SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim( ClaimTypes.Name,user.UserName),
                new Claim("PersonID",user.PersonID.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,

                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),

                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }






        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found.");

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }














        [Authorize]
        [HttpGet("TestAuth")]
        public IActionResult TestAuth()
        {
            return Ok("Authenticated");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("TestAdmin")]
        public IActionResult TestAdmin()
        {
            return Ok("Admin access");
        }










    }
}