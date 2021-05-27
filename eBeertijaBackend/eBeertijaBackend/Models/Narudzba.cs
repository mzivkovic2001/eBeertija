using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class Narudzba: BaseBoObject
    {
        public DateTime Datum { get; set; }
        public double UkupnoSaPdvom { get; set; }
        public double UkupnoBezPdva { get; set; }
        public double PdvIznos { get; set; }
        public int Broj { get; set; }
        public int StolId { get; set; }
        [ForeignKey("StolId")]
        public Stol Stol { get; set; }
        public bool IsOstvarenaNaRacunu { get; set; }
        public ICollection<StavkaNarudzbe> StavkeNarudzbe { get; set; }
    }
}
