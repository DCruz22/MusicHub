using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("User_MusicalStyles")]
    public class User_MusicalStyle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_MusicalStyleId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int MusicalStyleId { get; set; }

        [ForeignKey("MusicalStyleId")]
        public virtual MusicalStyle MusicalStyle { get; set; }
    }
}