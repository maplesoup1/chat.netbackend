
using Microsoft.EntityFrameworkCore;
using simplebackend.Entities;

namespace simplebackend.Data
{
    public class ApplicationDbContext  : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.User> Users { get; set; } = null!;
        public DbSet<Entities.Friendships> Friendships { get; set; } = null!;
        public DbSet<Entities.Conversations> Conversations { get; set; } = null!;
        public DbSet<Entities.Messages> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Conversations>(entity =>
            {
                entity.HasKey(e => e.Conversation_id);
                
                entity.HasOne(c => c.User1)
                    .WithMany(u => u.ConversationsAsUser1)
                    .HasForeignKey(c => c.User1_id)
                    .OnDelete(DeleteBehavior.Restrict); 
                
                entity.HasOne(c => c.User2)
                    .WithMany(u => u.ConversationsAsUser2)
                    .HasForeignKey(c => c.User2_id)
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.Message_id);
                entity.Property(e => e.Content).IsRequired();
                entity.HasOne(m => m.Sender)
                    .WithMany(u => u.SentMessages)
                    .HasForeignKey(m => m.Sender_id);
                entity.HasOne(m => m.Conversation)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(m => m.Conversation_id);
            });

            modelBuilder.Entity<Friendships>(entity =>
            {
                entity.HasKey(e => e.Friendship_id);

                entity.HasOne(f => f.User)
                    .WithMany(u => u.Friendships)
                    .HasForeignKey(f => f.User_id)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(f => f.Friend)
                    .WithMany()
                    .HasForeignKey(f => f.Friend_id)
                    .OnDelete(DeleteBehavior.Restrict); 
                entity.HasIndex(f => new { f.User_id, f.Friend_id }).IsUnique();
            });
        }
    }
}
