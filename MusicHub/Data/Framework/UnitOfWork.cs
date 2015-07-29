using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Models;

namespace MusicHub.Data.Framework
{
    class UnitOfWork
    {
        MusicHubDB context = null;

        public UnitOfWork()
        {
            context = new MusicHubDB();
        }
    }
}
