using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("Friendships")]
    public class Friendship
    {
        //1ra Tim 4:6-16
        public int FriendshipId { get; set; }

        public int UserId_following { get; set; }

        [ForeignKey("UserId_following")]
        public virtual User User_following { get; set; }

        public int UserId_follower { get; set; }

        [ForeignKey("UserId_follower")]
        public virtual User User_follower { get; set; }
    }
}