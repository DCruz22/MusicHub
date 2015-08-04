using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class UserProfileViewModel
    {
        public User User { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}