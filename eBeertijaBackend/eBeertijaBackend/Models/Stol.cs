using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class Stol: BaseBoObject
    {
        public string OznakaStola { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Orientation { get; set; }
        public string SerijskiBrojUredaja { get; set; }
        public ICollection<Narudzba> Narudzbe { get; set; }
        public ICollection<Racun> Racuni { get; set; }
    }
}
