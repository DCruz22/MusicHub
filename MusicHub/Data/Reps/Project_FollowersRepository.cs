using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class Project_FollowersRepository : Framework.BaseObject<Project_Follower>
	{
		
        public Project_FollowersRepository() : base() { }
        public Project_FollowersRepository(MusicHubDB cont) : base(cont) { }

        public override Project_Follower GetById(int id)
        {
            return db.Project_Followers.FirstOrDefault(x => x.Project_FollowerId == id);
        }
		
	}
}
