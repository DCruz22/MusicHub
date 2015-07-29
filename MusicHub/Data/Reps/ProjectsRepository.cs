using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class ProjectsRepository : Framework.BaseObject<Project>
	{
		
        public ProjectsRepository() : base() { }
        public ProjectsRepository(MusicHubDB cont) : base(cont) { }

        public override Project GetById(int id)
        {
            return db.Projects.FirstOrDefault(x => x.ProjectId == id);
        }
		
	}
}
