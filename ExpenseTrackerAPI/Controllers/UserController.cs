using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        if (userDto  == null)
        {
            return BadRequest("User data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return validation errors if there are any
        }


        var createdUser = await _userService.CreateUserAsync(userDto);

        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // GET: api/User
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (user == null || user.Id != id)
        {
            return BadRequest("User data is incorrect.");
        }

        var existingUser = await _userService.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(updatedUser);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
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
            return NoContent(); // Successfully deleted, no content in response
        }

        return BadRequest("Unable to delete user.");
    }
}
