using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class MusicalStylesRepository : Framework.BaseObject<MusicalStyle>
	{
		
        public MusicalStylesRepository() : base() { }
        public MusicalStylesRepository(MusicHubDB cont) : base(cont) { }

        public override MusicalStyle GetById(int id)
        {
            return db.MusicalStyles.FirstOrDefault(x => x.MusicalStyleId == id);
        }
		
	}
}
