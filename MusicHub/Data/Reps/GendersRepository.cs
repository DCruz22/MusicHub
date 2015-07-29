using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class GendersRepository : Framework.BaseObject<Gender>
	{
		
        public GendersRepository() : base() { }
        public GendersRepository(MusicHubDB cont) : base(cont) { }

        public override Gender GetById(int id)
        {
            return db.Genders.FirstOrDefault(x => x.GenderId == id);
        }
	}
}
