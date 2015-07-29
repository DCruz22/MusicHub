using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class User_InstrumentsRepository : Framework.BaseObject<User_Instrument>
	{
		
        public User_InstrumentsRepository() : base() { }
        public User_InstrumentsRepository(MusicHubDB cont) : base(cont) { }

        public override User_Instrument GetById(int id)
        {
            return db.User_Instruments.FirstOrDefault(x => x.User_InstrumentId == id);
        }
		
	}
}
