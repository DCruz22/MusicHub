using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace MusicHub.Models
{
    [Table("User_Badges")]
    public class User_Badge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_BadgeId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int BadgeId { get; set; }

        [ForeignKey("BadgeId")]
        public Badge Badge { get; set; }
    }
}