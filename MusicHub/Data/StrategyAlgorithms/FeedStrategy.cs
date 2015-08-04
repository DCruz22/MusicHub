using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MusicHub.Models;
using MusicHub.Data.Reps;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Data.StrategyAlgorithms
{
    public class FeedStrategy
    {
        private FeedsRepository _feedrep = new FeedsRepository();
        private FriendshipsRepository _friendrep = new FriendshipsRepository();

        public IEnumerable<Feed> GetUserFeed(int userId)
        {
            List<Friendship> friends = _friendrep.Filter(x => x.UserId_follower == userId).ToList();

            List<Feed> news = new List<Feed>();

            foreach(var friend in friends)
            {
                List<Feed> feeds = _feedrep.Filter(x => x.UserId == friend.UserId_followed).ToList();

                if(feeds != null)
                {
                    foreach(var feed in feeds)
                    {
                        news.Add(feed);
                    }
                }
            }

            return news;
        }

        public async Task<IEnumerable<Feed>> GetUserFeedAsync(int userId)
        {
            List<Friendship> friends = (await _friendrep.FilterAsync(x => x.UserId_follower == userId)).ToList();

            List<Feed> news = new List<Feed>();

            foreach (var friend in friends)
            {
                List<Feed> feeds = (await _feedrep.FilterAsync(x => x.UserId == friend.UserId_followed)).ToList();

                if (feeds != null)
                {
                    foreach (var feed in feeds)
                    {
                        news.Add(feed);
                    }
                }
            }

            return news;
        }
    }
}