using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class StavkaNarudzbe: BaseBoObject
    {
        public int NarudzbaId { get; set; }
        [ForeignKey("NarudzbaId")]
        public Narudzba Narudzba { get; set; }

        public int StavkaCjenikaId { get; set; }
        [ForeignKey("StavkaCjenikaId")]
        public StavkaCjenika StavkaCjenika { get; set; }
        public double JedinicnaCijenaStavke { get; set; }
        public int Kolicina { get; set; }
        public double Ukupno { get; set; }
    }
}
