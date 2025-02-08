using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);
        Task <User> IsAuthenticated(string userName, string password);
    }
}
