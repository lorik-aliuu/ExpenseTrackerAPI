using AutoMapper;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Services.Dtos;
using System.Net.NetworkInformation;

namespace ExpenseTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IExpenseRepository expenseRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(UserDto user)
        {
            return await _userRepository.AddUserAsync(_mapper.Map<User>(user));
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
