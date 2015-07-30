using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class ProjectViewModel
    {
        public User User { get; set; }
        public Project Project { get; set; }
    }
}