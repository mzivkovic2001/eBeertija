using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class Racun: BaseBoObject
    {
        public int Broj { get; set; }
        public DateTime Datum { get; set; }
        public int? NarudzbaId { get; set; }
        [ForeignKey("NarudzbaId")]
        public Narudzba Narudzba { get; set; }
        public double UkupnoSaPdvom { get; set; }
        public double UkupnoBezPdva { get; set; }
        public double IznosPdv { get; set; }
        public int StolId { get; set; }
        [ForeignKey("StolId")]
        public Stol Stol { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public bool IsStorniran { get; set; }
        public int? StorniraniRacunId { get; set; }
        [ForeignKey("StorniraniRacunId")]
        public Racun StorniraniRacun { get; set; }
        public string NazivKorisnika { get; set; }
        public ICollection<StavkaRacuna> StavkeRacuna { get; set; }
    }
}
