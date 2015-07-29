using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class Project_CommentViewModel
    {
        public Project_Comment  comment { get; set; }
        public User user { get; set; }
    }
}