using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class ProjectContentViewModel
    {
        public Project_Content New_Content { get; set; }
        public IEnumerable<Project_Content> Past_Contents { get; set; }
    }
}