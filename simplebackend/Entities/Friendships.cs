using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simplebackend.Entities
{   
    public enum FriendshipStatus
    {
        nonFriend,
        Pending,
        Friend,
        Blocked
    }
    public class Friendships
    {
        [Key]
        public int Friendship_id { get; set; }

        [ForeignKey("user_id")]
        public int User_id { get; set; }

        [ForeignKey("friend_id")]
        public int Friend_id { get; set; }

        public DateTime Created_at { get; set; }

        public FriendshipStatus Status { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual User Friend { get; set; } = null!;
    }
}