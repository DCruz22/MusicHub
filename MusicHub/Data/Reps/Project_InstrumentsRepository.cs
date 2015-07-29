using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class Project_InstrumentsRepository : Framework.BaseObject<Project_Instrument>
	{
		
        public Project_InstrumentsRepository() : base() { }
        public Project_InstrumentsRepository(MusicHubDB cont) : base(cont) { }

        public override Project_Instrument GetById(int id)
        {
            return db.Project_Instruments.FirstOrDefault(x => x.Project_InstrumentId == id);
        }
		
	}
}
