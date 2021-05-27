using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class StavkaCjenika: BaseBoObject
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int CjenikId { get; set; }
        [ForeignKey("CjenikId")]
        public Cjenik Cjenik { get; set; }
        public Kategorije Kategorija { get; set; }
        public double CijenaSaPdvom { get; set; }
        public double CijenaBezPdva { get; set; }
        public double PDV { get; set; }

    }

    public enum Kategorije
    {
        HOT = 1, COLD = 2, ALCOHOL = 3, ELSE = 4
    }
}
