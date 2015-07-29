using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MusicHub.Models;
using MusicHub.Data.Reps;

namespace MusicHub.Data.Reps.Reps
{ 

	public class CountriesRepository : Framework.BaseObject<Country>
	{
		
        public CountriesRepository() : base() { }
        public CountriesRepository(MusicHubDB cont) : base(cont) { }

        public override Country GetById(int id)
        {
            return db.Countries.FirstOrDefault(x => x.CountryId == id);
        }
		
	}
}
