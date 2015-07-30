using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class Project_CommentViewModel
    {
        public Project_Comment  Comment { get; set; }
        public User User { get; set; }
    }
}