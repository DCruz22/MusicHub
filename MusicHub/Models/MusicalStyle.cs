using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{

    [Table("MusicalStyles")]
    public class MusicalStyle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MusicalStyleId { get; set; }

        public string MusicalStyleName { get; set; }
    }
}