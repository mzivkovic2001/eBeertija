using ebeertijaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class RacunIzvjestajDto
    {
        public int Id { get; set; }
        public int Broj { get; set; }
        public string BrojOpis { get; set; }
        public DateTime Datum { get; set; }
        public int? NarudzbaId { get; set; }
        public NarudzbaDto Narudzba { get; set; }
        public double UkupnoSaPdvom { get; set; }
        public double UkupnoBezPdva { get; set; }
        public double IznosPdv { get; set; }
        public int StolId { get; set; }
        public string OznakaStola { get; set; }
        public int UserId { get; set; }
        public bool IsStorniran { get; set; }
        public int? StorniraniRacunId { get; set; }
        public string BrojStorniranogRacuna { get; set; }
        public string NazivKorisnika { get; set; }
        public ICollection<StavkaRacunaDto> StavkeRacuna { get; set; }
    }
}
