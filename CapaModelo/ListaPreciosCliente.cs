using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class ListaPreciosCliente
    {
        public int IdListaPrecios { get; set; }
        public Producto oProducto { get; set; }
        public Cliente oCliente { get; set; }
     
        public decimal PrecioCliente { get; set; }
       
    }
}
