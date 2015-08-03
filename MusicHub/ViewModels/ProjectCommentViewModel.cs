using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class ProjectCommentViewModel
    {
        public IEnumerable<Project_Comment> PastComments { get; set; }
        public Project_Comment NewComment { get; set; }
    }
}