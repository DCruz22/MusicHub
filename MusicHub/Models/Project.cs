using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Musical style")]
        public int MusicalStyleId { get; set; }

        [ForeignKey("MusicalStyleId")]
        public virtual MusicalStyle MusicalStyle { get; set; }
    }
}