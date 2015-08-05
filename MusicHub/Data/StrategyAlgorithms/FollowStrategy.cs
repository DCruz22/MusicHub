using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MusicHub.Models;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Data.StrategyAlgorithms
{
    public class FollowStrategy
    {
        private FriendshipsRepository _friendrep = new FriendshipsRepository();
        public IEnumerable<User> GetUserFollowing(int userId)
        {
            List<Friendship> following = (_friendrep.Filter(x => x.UserId_follower == userId)).ToList();

            List<User> users = new List<User>();

            foreach(Friendship friend in following)
            {
                users.Add(friend.User_followed);
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetUserFollowingAsync(int userId)
        {
            List<Friendship> following = (await _friendrep.FilterAsync(x => x.UserId_follower == userId)).ToList();

            List<User> users = new List<User>();

            foreach (Friendship friend in following)
            {
                users.Add(friend.User_followed);
            }

            return users;
        }

        public IEnumerable<User> GetUserFollowers(int userId)
        {
            List<Friendship> following = (_friendrep.Filter(x => x.UserId_followed == userId)).ToList();

            List<User> users = new List<User>();

            foreach (Friendship friend in following)
            {
                users.Add(friend.User_follower);
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetUserFollowersAsync(int userId)
        {
            List<Friendship> following = (await _friendrep.FilterAsync(x => x.UserId_followed == userId)).ToList();

            List<User> users = new List<User>();

            foreach (Friendship friend in following)
            {
                users.Add(friend.User_follower);
            }

            return users;
        }
    }
}