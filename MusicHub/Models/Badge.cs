using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("Badges")]
    public class Badge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BadgeId { get; set; }

        public string BadgeTitle { get; set; }

        public string BadgeDescription { get; set; }

        public string BadgeImageUrl { get; set; }
    }
}