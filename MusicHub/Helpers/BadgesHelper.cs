using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Helpers
{
    public class BadgesHelper
    {
        private UserRepository _usrep = new UserRepository();
        private BadgesRepository _badgerep = new BadgesRepository();
        private User_BadgesRepository _usrbadrep = new User_BadgesRepository();
    }
}