using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class StavkaRacuna: BaseBoObject
    {
        public int RacunId { get; set; }
        [ForeignKey("RacunId")]
        public Racun Racun { get; set; }

        public int StavkaCjenikaId { get; set; }
        [ForeignKey("StavkaCjenikaId")]
        public StavkaCjenika StavkaCjenika { get; set; }
        public double JedinicnaCijenaStavke { get; set; }
        public int Kolicina { get; set; }
        public double UkupnoSaPdvom { get; set; }
        public double UkupnoBezPdva { get; set; }
        public double IznosPdv { get; set; }
    }
}
