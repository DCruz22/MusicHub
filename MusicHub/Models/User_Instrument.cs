using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("User_Instruments")]
    public class User_Instrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_InstrumentId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int InstrumentId { get; set; }

        [ForeignKey("InstrumentId")]
        public virtual Instrument Instrument { get; set; }
    }
}