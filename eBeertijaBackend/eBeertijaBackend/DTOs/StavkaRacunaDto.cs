using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class StavkaRacunaDto
    {
        public int Id { get; set; }
        public int RacunId { get; set; }
        public int StavkaCjenikaId { get; set; }
        public double JedinicnaCijenaStavke { get; set; }
        public string NazivStavke { get; set; }
        public int Kolicina { get; set; }
        public double UkupnoSaPdvom { get; set; }
        public double UkupnoBezPdva { get; set; }
        public double IznosPdv { get; set; }
    }
}
