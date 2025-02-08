using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Dtos;

namespace ExpenseTrackerAPI.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto user);

        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task<UserDto> GetUserByIdAsync(int id);

        Task<UserDto> UpdateUserAsync(UserDto user);

        Task<bool> DeleteUserAsync(int id);

        Task<bool> IsAuthenticated(string userName, string password);

    }
}
