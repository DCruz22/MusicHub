using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Data.StrategyAlgorithms
{
    public class SearchProjectsAlgorithm
    {
        public string word { get; set; }

        private ProjectsRepository _projrep = new ProjectsRepository();
        private UserRepository _usrrep = new UserRepository();

        public SearchProjectsAlgorithm(string word)
        {
            this.word = word;
        }

        public IEnumerable<ProjectViewModel> SearchProjects()
        {
            List<Project> projects = _projrep.Filter(x => x.ProjectName.Contains(word))
                                     .OrderByDescending(x => x.CreationDate)
                                     .ToList();

            List<ProjectViewModel> projvm = new List<ProjectViewModel>();

            foreach (Project proj in projects )
            {
                User usr = _usrrep.Find(x => x.UserId == proj.UserId);

                projvm.Add(new ProjectViewModel()
                {
                    project = proj,
                    user = usr
                });
            }

            projvm = projvm.OrderByDescending(x => x.project.CreationDate).ToList();

            return projvm;
        }

        public async Task<IEnumerable<ProjectViewModel>> SearchProjectsAsync()
        {
            List<Project> projects = (await _projrep.FilterAsync(x => x.ProjectName.Contains(word)))
                                            .OrderByDescending(x => x.CreationDate)
                                            .ToList();

            List<ProjectViewModel> projvm = new List<ProjectViewModel>();

            foreach(Project proj in projects)
            {
                User usr = await _usrrep.FindAsync(x => x.UserId == proj.UserId);

                projvm.Add(new ProjectViewModel()
                {
                    project = proj,
                    user = usr
                });
            }

            projvm = projvm.OrderByDescending(x => x.project.CreationDate).ToList();

            return projvm;
        }
    }
}