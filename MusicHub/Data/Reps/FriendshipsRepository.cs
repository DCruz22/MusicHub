using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class FriendshipsRepository : Framework.BaseObject<Friendship>
	{
		
        public FriendshipsRepository() : base() { }
        public FriendshipsRepository(MusicHubDB cont) : base(cont) { }

        public override Friendship GetById(int id)
        {
            return db.Friendships.FirstOrDefault(x => x.FriendshipId == id);
        }
		
	}
}
