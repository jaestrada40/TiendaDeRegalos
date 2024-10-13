using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaDeRegalo.Clases
{
    public class Transaccion
    {
        public List<Producto> ProductosComprados { get; private set; }
        public decimal Total { get; private set; }

        public Transaccion(List<Producto> productosComprados, decimal total)
        {
            ProductosComprados = productosComprados;
            Total = total;
        }
    }
}
