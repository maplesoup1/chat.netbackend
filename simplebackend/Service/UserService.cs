using simplebackend.Entities;
using simplebackend.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace simplebackend.services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _userRepository.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                throw;
            }
        }

        public async Task<User> CreateUserAsync(string username, string email, string password)
        {
            try
            {
                var user = new User
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                return await _userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with username {Username} and email {Email}", username, email);
                throw;
            }
        }

        public async Task<User> UpdateUserAsync(int userId, string username, string email, string password)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return null;
                }
                user.Username = username;
                user.Email = email;
                user.Password = password;
                return await _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return false;
                }
                await _userRepository.DeleteUser(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {UserId}", userId);
                throw;
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword;
        }

        public async Task<User> AuthenticateAsync(string usernameOrEmail, string password)
        {
            try
            {
                User user = await _userRepository.GetByUsernameAsync(usernameOrEmail);

                if (user == null)
                {
                    user = await _userRepository.GetByEmailAsync(usernameOrEmail);
                }
                if (user == null || !VerifyPassword(password, user.Password))
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during authentication for {UsernameOrEmail}", usernameOrEmail);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUserFriendsAsync(int userId)
        {
            try
            {
                return await _userRepository.GetFriends(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving friends for user with ID {UserId}", userId);
                throw;
            }

        }

        public async Task<IEnumerable<Conversations>> GetUserConversationsAsync(int userId)
        {
            try
            {
                return await _userRepository.GetConversationsAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving conversations for user with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Messages>> GetConversationMessagesAsync(int conversationId)
        {
            try
            {
                return await _userRepository.GetMessagesAsync(conversationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving messages for conversation with ID {ConversationId}", conversationId);
                throw;
            }
        }
    }

}