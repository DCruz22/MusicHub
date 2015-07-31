using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MusicHub.Models;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Controllers
{
    public class InstrumentsController : ApiController
    {

        private InstrumentsRepository _instrep = new InstrumentsRepository();
        // GET: api/Instruments
        public IEnumerable<Instrument> Get()
        {
            IEnumerable<Instrument> instruments = _instrep.All().ToList();
            return instruments;
        }

        // GET: api/Instruments/5
        public IEnumerable<Instrument> Get(int id)
        {
            return null;
        }

        // POST: api/Instruments
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Instruments/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Instruments/5
        public void Delete(int id)
        {
        }
    }
}
