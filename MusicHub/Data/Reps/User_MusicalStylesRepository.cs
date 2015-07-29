using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class User_MusicalStylesRepository : Framework.BaseObject<User_MusicalStyle>
	{
		
        public User_MusicalStylesRepository() : base() { }
        public User_MusicalStylesRepository(MusicHubDB cont) : base(cont) { }

        public override User_MusicalStyle GetById(int id)
        {
            return db.User_MusicalStyles.FirstOrDefault(x => x.User_MusicalStyleId == id);
        }
		
	}
}
