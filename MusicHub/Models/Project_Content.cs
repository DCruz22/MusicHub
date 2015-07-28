using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Models
{

    public enum ContentType 
    {
        [Display(Name = "Image")]
        Image = 0,
        [Display(Name = "Audio")]
        Audio = 1,
        [Display(Name = "Music notation")]
        Music_Notation = 2
    }

    [Table("Project_Contents")]
    public class Project_Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Project_ContentId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public string Content { get; set; }

        public ContentType ContentType { get; set; }
    }
}