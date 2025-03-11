using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simplebackend.Entities
{
    public class Messages
    {
        [Key]
        public int Message_id { get; set; }
        [ForeignKey("Conversation_id")]
        public int Conversation_id { get; set; }
        [ForeignKey("Sender_id")]
        public int Sender_id { get; set; }
        public required string Content { get; set; }
        public DateTime Sent_at { get; set; }
        public virtual Conversations Conversation { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}