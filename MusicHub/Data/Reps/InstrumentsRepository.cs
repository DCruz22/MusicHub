using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Data.Reps;
using MusicHub.Models;

namespace MusicHub.Data.Reps.Reps
{ 

	public class InstrumentsRepository : Framework.BaseObject<Instrument>
	{
		
        public InstrumentsRepository() : base() { }
        public InstrumentsRepository(MusicHubDB cont) : base(cont) { }

        public override Instrument GetById(int id)
        {
            return db.Instruments.FirstOrDefault(x => x.InstrumentId == id);
        }
		
	}
}
