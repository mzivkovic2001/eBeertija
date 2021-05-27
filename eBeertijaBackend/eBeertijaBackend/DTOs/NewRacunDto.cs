using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class NewRacunDto
    {
        public int Broj { get; set; }
        public string BrojOpis { get; set; }
        public List<StavkaCjenikaDto> StavkeCjenikaNaSnazi { get; set; }
        public List<StavkaRacunaDto> StavkeRacuna { get; set; }
        public int StolId { get; set; }
        public string OznakaStola { get; set; }
    }
}
