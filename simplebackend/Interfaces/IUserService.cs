using simplebackend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simplebackend.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(string username, string email, string password);
        Task<User> UpdateUserAsync(int userId, string username, string email, string password);
        Task<bool> DeleteUserAsync(int userId);
        
        Task<User> AuthenticateAsync(string usernameOrEmail, string password);
        Task<IEnumerable<User>> GetUserFriendsAsync(int userId);
        Task<IEnumerable<Conversations>> GetUserConversationsAsync(int userId);
        Task<IEnumerable<Messages>> GetConversationMessagesAsync(int conversationId);
    }
}