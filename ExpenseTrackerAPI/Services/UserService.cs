using AutoMapper;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Services.Dtos;

namespace ExpenseTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var createdUser = await _userRepository.AddUserAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

          
            _mapper.Map(userDto, existingUser);

            await _userRepository.UpdateUserAsync(existingUser);

            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> IsAuthenticated(string userName, string password)
        {
            var user = await _userRepository.IsAuthenticated(userName, password);

            if (user != null)
                return true;
            return false;

        }
    }
}
