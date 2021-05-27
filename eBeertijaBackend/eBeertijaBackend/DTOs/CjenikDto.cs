using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class CjenikDto
    {
        public int Id { get; set; }
        public DateTime DatumPocetkaPrimjene { get; set; }
        public ICollection<StavkaCjenikaDto> StavkeCjenika { get; set; }
        public bool IsEditable { get; set; }
    }
}
