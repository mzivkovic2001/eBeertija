using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class Cjenik: BaseBoObject
    {
        public DateTime DatumPocetkaPrimjene { get; set; }
        public ICollection<StavkaCjenika> StavkeCjenika { get; set; }
    }
}
