using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaDeRegalo.Clases
{
    public class Inventario
    {
        private List<Producto> productos = new List<Producto>();

        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }

        public void EliminarProducto(string nombre)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre == nombre);
            if (producto != null)
            {
                productos.Remove(producto);
            }
        }

        public void ActualizarProducto(string nombre, int nuevaCantidad)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre == nombre);
            if (producto != null)
            {
                producto.Cantidad = nuevaCantidad;
            }
        }

        public Producto ObtenerProducto(string nombre)
        {
            return productos.FirstOrDefault(p => p.Nombre == nombre);
        }

        public List<Producto> ListarProductos()
        {
            return productos;
        }
    }
}
