using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class DetalleCobro
    {
        public Cobro oCobro { get; set; }
        public float ImporteTotal { get; set; }
        public string TextoImporteTotal { get; set; }
        public string FechaCobro { get; set; }
        public DateTime VFechaCobro { get; set; }


    }
}
