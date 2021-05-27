using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class GrafickiPrikazDto
    {
        public List<StolDto> AktivniStolovi { get; set; }
        public List<StolDto> ObrisaniStolovi { get; set; }
    }
}
