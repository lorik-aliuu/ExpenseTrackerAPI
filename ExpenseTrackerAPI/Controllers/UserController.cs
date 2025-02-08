using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await _userService.CreateUserAsync(userDto);

        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }


    [HttpGet("getbyid")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }


    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        var updatedUser = await _userService.UpdateUserAsync(userDto);
        return Ok(updatedUser);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var isDeleted = await _userService.DeleteUserAsync(id);
        if (isDeleted)
        {
            return Ok("deleted");
        }

        return BadRequest("Unable to delete user.");
    }



    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto userDto)
    {
        if (userDto is null)
        {
            return BadRequest("Invalid client request");
        }

        var user = await _userService.IsAuthenticated(userDto.Username, userDto.Password);
        if (user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345wedr2342423t2e32432rtfedqwqwer"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }
        return Unauthorized();
    }
}
