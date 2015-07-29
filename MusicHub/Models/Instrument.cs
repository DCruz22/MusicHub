using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    public enum InstrumentTypes 
    {   
        [Display(Name="Strings")]
        Strings = 0,
        
        [Display(Name="WoodWinds")]
        WoodWinds = 1,

        [Display(Name="Brass")]
        Brass = 2,

        [Display(Name="Percussion")]
        Percussion = 3,
        
        [Display(Name="Electronic")]
        Electronic = 4
    }

    [Table("Instruments")]
    public class Instrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstrumentId { get; set; }
        [Required]
        public string InstrumentName { get; set; }
        public InstrumentTypes InstrumentType { get; set; }
    }
}