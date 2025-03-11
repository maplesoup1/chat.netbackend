using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace simplebackend.Entities
{
    public class User
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }


        public virtual ICollection<Messages> SentMessages { get; set; } = new List<Messages>();
        public virtual ICollection<Conversations> ConversationsAsUser1 { get; set; } = new List<Conversations>();
        public virtual ICollection<Conversations> ConversationsAsUser2 { get; set; } = new List<Conversations>(); 
        public virtual ICollection<Friendships> Friendships { get; set; } = new List<Friendships>();
    }
}