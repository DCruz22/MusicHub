using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Data.Reps.Reps;
using MusicHub.Models;
using Microsoft.AspNet.SignalR;
using MusicHub.Hubs;

namespace MusicHub.Hubs
{
    public class MusicHub : Hub
    {

        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        private const string DOMAIN = "http://musichub.com/";

        private FriendshipsRepository _friendrep = new FriendshipsRepository();
        private UserRepository _usrep = new UserRepository();
        private NotificationsRepository _notrep = new NotificationsRepository();

        public void Hello()
        {
            Clients.All.hello();
        }

        public async void notifyFollowerFriendShip(int userFollowerId, int userFollowedId, string html)
        {
            User user = await _usrep.FindAsync(a => a.UserId == userFollowerId);

            foreach (var connectionId in _connections.GetConnections(user.UserName))
            {
                if (connectionId == this.Context.ConnectionId)
                    continue;

                Clients.Client(connectionId).appendFollowerFriendship(userFollowerId, userFollowedId, html);
            }
        }

        public async void notifyFollowedFriendShip(int userFollowedId, int userFollowerId)
        {
            User userFollowed = await _usrep.FindAsync(a => a.UserId == userFollowedId);
            User userFollower = await _usrep.FindAsync(a => a.UserId == userFollowerId);
            IEnumerable<Friendship> friendship = await _friendrep.FilterAsync(a => a.UserId_followed == userFollowedId && a.UserId_follower == userFollowerId);

            //The letters stand for Follow or Unfollow
            string action = (friendship.Count() > 0 ? "F" : "U");

            string fullUrl = DOMAIN + "User/Notebook?userName=" + userFollower.UserName;

            foreach (var connectionId in _connections.GetConnections(userFollowed.UserName))
            {
                Clients.Client(connectionId).appendFollowedFriendship(action, userFollower.UserName, fullUrl);
            }

            await _notrep.CreateAsync(new Notification()
            {
                UserId = userFollowed.UserId,
                NotificationDescription = userFollower.UserName + (action == "F" ? " is now Following You " : " is not following you anymore"),
                Url = fullUrl,
                Date = DateTime.Now
            });
        }

    }
}