using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaDeRegalo.Clases
{
    public class CarritoCompra
    {
        private List<Producto> productos = new List<Producto>();

        public void AgregarProducto(Producto producto, int cantidad)
        {
            if (cantidad > producto.Cantidad)
            {
                throw new InvalidOperationException("No hay suficiente cantidad en inventario.");
            }

            productos.Add(new Producto(producto.Nombre, producto.Precio, cantidad));
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in productos)
            {
                total += producto.Precio * producto.Cantidad;
            }
            return total * 1.12m; // Agregar 12% de impuesto
        }

        public List<Producto> ObtenerProductos()
        {
            return productos;
        }
    }
}
