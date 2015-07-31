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
    public class MusicalStyleController : ApiController
    {

        private MusicalStylesRepository _stylerep = new MusicalStylesRepository();
        // GET: api/MusicalStyle
        public IEnumerable<MusicalStyle> Get()
        {
            IEnumerable<MusicalStyle> styles = _stylerep.All().ToList();
            return styles;
        }

        // GET: api/MusicalStyle/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MusicalStyle
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MusicalStyle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MusicalStyle/5
        public void Delete(int id)
        {
        }
    }
}
