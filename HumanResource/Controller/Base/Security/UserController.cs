using Entities.Basic.Security;
using Entities.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResource.Controller.Security
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly DAL.AppDbContext _context;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            DAL.AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles
                .AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name
                })
                .ToListAsync();

            return Ok(roles);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            var personExists = _context.Set<Entities.Basic.Personel.Person>()
                .Any(x => x.ID == model.PersonID);

            if (!personExists)
                return BadRequest("Person not found.");

            var personHasUser = _userManager.Users
                .Any(x => x.PersonID == model.PersonID);

            if (personHasUser)
                return BadRequest("This person already has a user.");

            var existingUser = await _userManager.FindByNameAsync(model.UserName);

            if (existingUser != null)
                return BadRequest("Username already exists.");

            if (!await _roleManager.RoleExistsAsync(model.RoleName))
                return BadRequest("Role not found.");

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                PersonID = model.PersonID
            };

            var result = await _userManager.CreateAsync(
                user,
                model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                model.RoleName);

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.PersonID,
                Role = model.RoleName
            });
        }



        [HttpPost("{userId}/Role/{roleName}")]
        public async Task<IActionResult> AddRole(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound("User not found.");

            if (!await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role not found.");

            if (await _userManager.IsInRoleAsync(user, roleName))
                return BadRequest("User already has this role.");

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }



        [HttpDelete("{userId}/Role/{roleName}")]
        public async Task<IActionResult> RemoveRole(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound("User not found.");

            if (!await _userManager.IsInRoleAsync(user, roleName))
                return BadRequest("User does not have this role.");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }





        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Select(user => new
                {
                    user.Id,
                    user.UserName,
                    user.PersonID,

                    Person = new
                    {
                        user.Person.FirstName,
                        user.Person.LastName
                    },

                    Roles = _context.UserRoles
                        .Where(userRole => userRole.UserId == user.Id)
                        .Join(
                            _context.Roles,
                            userRole => userRole.RoleId,
                            role => role.Id,
                            (userRole, role) => role.Name
                        )
                        .ToList()
                })
                .ToListAsync();

            return Ok(users);
        }




        [HttpPut("{userId}/Disable")]
        public async Task<IActionResult> DisableUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound("User not found.");

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }


        [HttpPut("{userId}/Enable")]
        public async Task<IActionResult> EnableUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound("User not found.");

            user.LockoutEnd = null;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(
                model.UserId.ToString());

            if (user == null)
                return NotFound("User not found.");

            var token = await _userManager
                .GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(
                user,
                token,
                model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.PersonID,
                    x.LockoutEnabled,
                    x.LockoutEnd,

                    Person = new
                    {
                        x.Person.FirstName,
                        x.Person.LastName
                    },

                    Roles = _context.UserRoles
                        .Where(ur => ur.UserId == x.Id)
                        .Join(
                            _context.Roles,
                            ur => ur.RoleId,
                            role => role.Id,
                            (ur, role) => new
                            {
                                role.Id,
                                role.Name
                            })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }



        [HttpGet("{userId}/Roles")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var user = await _userManager.FindByIdAsync(
                userId.ToString());

            if (user == null)
                return NotFound("User not found.");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }


        [HttpGet("SearchByPersonCode")]
        public async Task<IActionResult> SearchByPersonCode([FromQuery] string personCode)
        {
            if (string.IsNullOrWhiteSpace(personCode))
            {
                return BadRequest(new
                {
                    message = "کد پرسنلی الزامی است."
                });
            }

            personCode = personCode.Trim();

            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Person.PersonCode == personCode)
                .Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.PersonID,
                    PersonCode = x.Person.PersonCode,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    FullName =  x.Person.FirstName + " " + x.Person.LastName,
                    IsDisabled = x.LockoutEnd != null &&  x.LockoutEnd > DateTimeOffset.UtcNow,
                    Roles = _context.UserRoles
                        .Where(ur => ur.UserId == x.Id)
                        .Join(
                            _context.Roles,
                            ur => ur.RoleId,
                            role => role.Id,
                            (ur, role) => role.Name
                        )
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new{  message = "برای این کد پرسنلی کاربری پیدا نشد."  });
            }

            return Ok(user);
        }




        [HttpPut("ChangeRole")]
        public async Task<IActionResult> ChangeRole(ChangeUserRoleDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            if (user == null)
            {
                return NotFound(new { message = "کاربر پیدا نشد."  });
            }

            if (!await _roleManager
                .RoleExistsAsync(model.RoleName))
            {
                return BadRequest(new { message = "نقش موردنظر وجود ندارد." });
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Contains(model.RoleName))
            {
                return BadRequest(new { message = "کاربر در حال حاضر همین نقش را دارد." });
            }

            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user,currentRoles);

                if (!removeResult.Succeeded)
                    return BadRequest(removeResult.Errors);
            }

            var addResult =await _userManager.AddToRoleAsync(user,model.RoleName);

            if (!addResult.Succeeded)
                return BadRequest(addResult.Errors);

            return Ok(new { message = "نقش کاربر با موفقیت تغییر کرد." });
        }






    }
}