using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.ViewModels;

namespace MusicHub.Data.StrategyAlgorithms
{
    public class SearchProjectsAlgorithm
    {
        public string word { get; set; }

        public SearchProjectsAlgorithm(string word)
        {
            this.word = word;
        }

        public IEnumerable<ProjectViewModel> SearchProjects()
        {
            return null;
        }

        public Task<IEnumerable<ProjectViewModel>> SearchProjects()
        {
            return null;
        }
    }
}