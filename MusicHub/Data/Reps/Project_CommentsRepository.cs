using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class Project_CommentsRepository : Framework.BaseObject<Project_Comment>
	{
		
        public Project_CommentsRepository() : base() { }
        public Project_CommentsRepository(MusicHubDB cont) : base(cont) { }

        public override Project_Comment GetById(int id)
        {
            return db.Project_Comments.FirstOrDefault(x => x.Project_CommentId == id);
        }
		
	}
}
