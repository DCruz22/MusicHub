using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class Project_ContentsRepository : Framework.BaseObject<Project_Content>
	{
		
        public Project_ContentsRepository() : base() { }
        public Project_ContentsRepository(MusicHubDB cont) : base(cont) { }

        public override Project_Content GetById(int id)
        {
            return db.Project_Content.FirstOrDefault(x => x.Project_ContentId == id);
        }
		
	}
}
