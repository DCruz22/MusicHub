using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class User_BadgesRepository : Framework.BaseObject<User_Badge>
	{
		
        public User_BadgesRepository() : base() { }
        public User_BadgesRepository(MusicHubDB cont) : base(cont) { }

        public override User_Badge GetById(int id)
        {
            return db.User_Badges.FirstOrDefault(x => x.User_BadgeId == id);
        }
		
	}
}
