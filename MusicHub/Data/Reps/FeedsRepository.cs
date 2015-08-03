using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.Data.Reps
{
    public class FeedsRepository : Framework.BaseObject<Feed>
    {
        public FeedsRepository() { }

        public FeedsRepository(MusicHubDB cont) : base(cont) { }

        public override Feed GetById(int id)
        {
            return db.Feeds.FirstOrDefault(x => x.FeedId == id);
        }
    }
}