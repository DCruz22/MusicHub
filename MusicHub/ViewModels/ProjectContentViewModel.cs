using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class ProjectContentViewModel
    {
        public Project project { get; set; }
        public IEnumerable<Project_Content> contents { get; set; }
    }
}