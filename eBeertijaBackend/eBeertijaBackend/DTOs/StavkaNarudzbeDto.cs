using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class StavkaNarudzbeDto
    {
        public int NarudzbaId { get; set; }
        public int StavkaCjenikaId { get; set; }
        public double JedinicnaCijenaStavke { get; set; }
        public string NazivStavke { get; set; }
        public int Kolicina { get; set; }
        public double Ukupno { get; set; }
    }
}
