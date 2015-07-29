using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class UsersRepository : Framework.BaseObject<User>
	{
		
        public UsersRepository() : base() { }
        public UsersRepository(MusicHubDB cont) : base(cont) { }

        public override User GetById(int id)
        {
            return db.Users.FirstOrDefault(x => x.UserId == id);
        }
		
	}
}
