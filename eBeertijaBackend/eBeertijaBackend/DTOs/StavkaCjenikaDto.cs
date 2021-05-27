using ebeertijaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class StavkaCjenikaDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int CjenikId { get; set; }
        public Kategorije Kategorija { get; set; }
        public double CijenaSaPdvom { get; set; }
        public double CijenaBezPdva { get; set; }
        public double PDV { get; set; }
    }
}
