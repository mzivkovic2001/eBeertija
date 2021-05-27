using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class StolDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Orientation { get; set; }
        public string OznakaStola { get; set; }
        public string StolClass { get; set; }
        public string SerijskiBrojUredaja { get; set; }
        public int? NarudzbaId { get; set; }
        public int? PrioritetNarudzbe { get; set; }

        public int? YOffset { get; set; }
        public int? XOffset { get; set; }
    }
}
