using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class UserRepository : Framework.BaseObject<User>
	{
		
        public UserRepository() : base() { }
        public UserRepository(MusicHubDB cont) : base(cont) { }

        public override User GetById(int id)
        {
            return db.Users.FirstOrDefault(x => x.UserId == id);
        }
		
	}
}
