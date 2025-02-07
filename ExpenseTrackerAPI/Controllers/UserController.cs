using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("AddUser")]
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


    [HttpGet("GetByID")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }


    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }


    [HttpPut("UpdateUserById")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
    {
        if (userDto == null || userDto.Id != id)
        {
            return BadRequest("User data is incorrect.");
        }

        var existingUser = await _userService.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        var updatedUser = await _userService.UpdateUserAsync(userDto); 
        return Ok(updatedUser);
    }

    [HttpDelete("DeleteUserByID")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var isDeleted = await _userService.DeleteUserAsync(id);
        if (isDeleted)
        {
            return NoContent(); 
        }

        return BadRequest("Unable to delete user.");
    }
}
