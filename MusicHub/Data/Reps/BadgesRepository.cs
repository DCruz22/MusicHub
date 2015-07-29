using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class BadgesRepository : Framework.BaseObject<Badge>
	{
		
        public BadgesRepository() : base() { }
        public BadgesRepository(MusicHubDB cont) : base(cont) { }

        public override Badge GetById(int id)
        {
            return db.Badges.FirstOrDefault(x => x.BadgeId == id);
        }
		
	}
}
