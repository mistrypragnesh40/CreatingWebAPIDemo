using CRUDOperationUsingWEBAPI.Data;
using CRUDOperationUsingWEBAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDOperationUsingWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UsersController(UserManager<Users> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO roleDTO)
        {

            var response = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleDTO.RoleName
            });

            if (response.Succeeded)
            {
                return Ok("New Role Created");
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }


        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserDTO assignRoleToUserDTO)
        {

            var userDetails = await _userManager.FindByEmailAsync(assignRoleToUserDTO.Email);

            if (userDetails != null)
            {

                var userRoleAssignResponse = await _userManager.AddToRoleAsync(userDetails, assignRoleToUserDTO.RoleName);

                if (userRoleAssignResponse.Succeeded)
                {
                    return Ok("Role Assigned to User: " + assignRoleToUserDTO.RoleName);
                }
                else
                {
                    return BadRequest(userRoleAssignResponse.Errors);
                }
            }
            else
            {
                return BadRequest("There are no user exist with this email");
            }


        }

        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUser authenticateUser)
        {
            var user = await _userManager.FindByNameAsync(authenticateUser.UserName);
            if (user == null) return Unauthorized();

            bool isValidUser = await _userManager.CheckPasswordAsync(user, authenticateUser.Password);

            if (isValidUser)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                };


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _configuration["JWT:Audience"],
                    Issuer = _configuration["JWT:Issuer"],
                    Expires = DateTime.UtcNow.AddDays(5),
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }

        }


        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {

            var userToBeCreated = new Users
            {
                Email = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                UserName = registerUserDTO.Email,
                Address = registerUserDTO.Address,
                Gender = registerUserDTO.Gender
            };

            var response = await _userManager.CreateAsync(userToBeCreated, registerUserDTO.Password);
            if (response.Succeeded)
            {
                return Ok("User created");
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserDTO userDetails)
        {

            var existingUser = await _userManager.FindByEmailAsync(userDetails.Email);
            if (existingUser != null)
            {
                var response = await _userManager.DeleteAsync(existingUser);

                if (response.Succeeded)
                {
                    return Ok("User Deleted");
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            else
            {
                return BadRequest("No User found with this email");
            }
        }
    }
}
