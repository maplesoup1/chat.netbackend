using System;
using simplebackend.Entities;
using System.Collections.Generic; 
using System.Threading.Tasks; 

namespace simplebackend.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);

        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<bool> EmailExists(string email);
        Task<bool> UsernameExists(string username);
        
        Task<IEnumerable<User>> GetFriends(int userId);
        Task<IEnumerable<Conversations>> GetConversationsAsync(int userId);
        Task<IEnumerable<Messages>> GetMessagesAsync(int conversationId);
    }
}