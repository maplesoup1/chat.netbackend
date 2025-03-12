using simplebackend.Data;
using simplebackend.Entities;
using simplebackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<User> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetFriends(int userId)
        {
            return await _context.Friendships
                .Where(f => f.Friend_id == userId)
                .Include(f => f.User)
                .Select(f => f.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Conversations>> GetConversationsAsync(int userId)
        {
            return await _context.Conversations
                .Where(c => c.User1_id == userId || c.User2_id == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Messages>> GetMessagesAsync(int conversationId)
        {
            return await _context.Messages
                .Where(m => m.Conversation_id == conversationId)
                .ToListAsync();
        }
    }
}