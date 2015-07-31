using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;

namespace MusicHub.ViewModels
{
    public class PreferencesViewModel
    {
        public List<MusicalStyle> MusicalStyles { get; set; }
        public List<Instrument> Instruments { get; set; }
    }
}