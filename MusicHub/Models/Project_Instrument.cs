using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("Project_Instruments")]
    public class Project_Instrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Project_InstrumentId { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project_Id { get; set; }

        public int InstrumentId { get; set; }

        [ForeignKey("InstrumentId")]
        public virtual Instrument Instrument { get; set; }
    }
}