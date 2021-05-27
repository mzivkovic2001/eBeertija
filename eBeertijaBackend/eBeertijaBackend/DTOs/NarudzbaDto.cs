using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class NarudzbaDto
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public double UkupnoSaPdvom { get; set; }
        public double UkupnoBezPdva { get; set; }
        public double PdvIznos { get; set; }
        public int Broj { get; set; }
        public string SerijskiBrojUredaja { get; set; }
        public string OznakaStola { get; set; }
        public ICollection<StavkaNarudzbeDto> StavkeNarudzbe { get; set; }
    }
}
