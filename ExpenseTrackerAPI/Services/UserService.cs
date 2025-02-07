using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;

        public UserService(IUserRepository userRepository, IExpenseRepository expenseRepository)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null)
                throw new KeyNotFoundException("Përdoruesi nuk u gjet.");

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException("Përdoruesi nuk u gjet.");

            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
