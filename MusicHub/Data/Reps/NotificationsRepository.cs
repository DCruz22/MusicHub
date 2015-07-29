using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class NotificationsRepository : Framework.BaseObject<Notification>
	{
		
        public NotificationsRepository() : base() { }
        public NotificationsRepository(MusicHubDB cont) : base(cont) { }

        public override Notification GetById(int id)
        {
            return db.Notification.FirstOrDefault(x => x.NotificationId == id);
        }
		
	}
}
