using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);

        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);

    }
}
