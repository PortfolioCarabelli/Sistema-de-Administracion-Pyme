using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class CuentaCorriente
    {
        public int IdCuentaCorriente { get; set; }
        public decimal Monto { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdCliente { get; set; }
    }
}
