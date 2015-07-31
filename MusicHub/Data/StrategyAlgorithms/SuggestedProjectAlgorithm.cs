using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Data.StrategyAlgorithms
{
    public class SuggestedProjectAlgorithm
    {
        private UserRepository _usrep = new UserRepository();
        private ProjectsRepository _projrep = new ProjectsRepository();
        private User_InstrumentsRepository userInstRep = new User_InstrumentsRepository();

        public static IEnumerable<ProjectViewModel> GetSuggestedProjects(string username)
        {
            return null;
        }

        public static async Task<IEnumerable<ProjectViewModel>> GetSuggestedProjectsAsync(string username)
        {
            return null;
        }
    }
}