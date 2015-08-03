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
        public string filter { get; set; }

        private ProjectsRepository _projrep = new ProjectsRepository();
        private UserRepository _usrrep = new UserRepository();

        public SearchProjectsAlgorithm(string filter)
        {
            this.filter = filter;
        }

        public IEnumerable<ProjectViewModel> SearchProjects()
        {
            List<Project> projects = _projrep.Filter(x => x.ProjectName.Contains(filter))
                                     .OrderByDescending(x => x.CreationDate)
                                     .ToList();

            List<ProjectViewModel> projvm = new List<ProjectViewModel>();

            foreach (Project proj in projects )
            {
                User usr = _usrrep.Find(x => x.UserId == proj.UserId);

                projvm.Add(new ProjectViewModel()
                {
                    Project = proj,
                    User = usr
                });
            }

            projvm = projvm.OrderByDescending(x => x.Project.CreationDate).ToList();

            return projvm;
        }

        public async Task<IEnumerable<ProjectViewModel>> SearchProjectsAsync()
        {
            List<Project> projects = (await _projrep.FilterAsync(x => x.ProjectName.Contains(filter)))
                                            .OrderByDescending(x => x.CreationDate)
                                            .ToList();

            List<ProjectViewModel> projvm = new List<ProjectViewModel>();

            foreach(Project proj in projects)
            {
                User usr = await _usrrep.FindAsync(x => x.UserId == proj.UserId);

                projvm.Add(new ProjectViewModel()
                {
                    Project = proj,
                    User = usr
                });
            }

            projvm = projvm.OrderByDescending(x => x.Project.CreationDate).ToList();

            return projvm;
        }
    }
}