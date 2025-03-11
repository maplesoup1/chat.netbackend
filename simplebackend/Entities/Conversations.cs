

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simplebackend.Entities
{
    public class Conversations
    {
        [Key]
        public int Conversation_id { get; set; }
        [ForeignKey("User1_id")]
        public int User1_id { get; set; }

        [ForeignKey("User2_id")]
        public int User2_id { get; set; }

        public DateTime Created_at { get; set; }
        public virtual User User1 { get; set; } = null!;
        public virtual User User2 { get; set; } = null!;
        public virtual ICollection<Messages> Messages { get; set; } = new List<Messages>();
    }
}