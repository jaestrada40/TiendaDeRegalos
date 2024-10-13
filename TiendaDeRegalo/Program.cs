using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaDeRegalo.Clases;

namespace TiendaDeRegalo
{
    internal class Program
    {
        static Inventario inventario = new Inventario();
        static HistorialTransacciones historial = new HistorialTransacciones();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menú Principal:");
                Console.WriteLine("1. Agregar Producto");
                Console.WriteLine("2. Eliminar Producto");
                Console.WriteLine("3. Actualizar Producto");
                Console.WriteLine("4. Realizar Compra");
                Console.WriteLine("5. Ver Historial de Transacciones");
                Console.WriteLine("6. Salir");

                var opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        AgregarProducto();
                        break;
                    case "2":
                        EliminarProducto();
                        break;
                    case "3":
                        ActualizarProducto();
                        break;
                    case "4":
                        RealizarCompra();
                        break;
                    case "5":
                        VerHistorial();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void AgregarProducto()
        {
            Console.Write("Nombre del producto: ");
            var nombre = Console.ReadLine();
            Console.Write("Precio del producto: ");
            var precio = decimal.Parse(Console.ReadLine());
            Console.Write("Cantidad del producto: ");
            var cantidad = int.Parse(Console.ReadLine());

            inventario.AgregarProducto(new Producto(nombre, precio, cantidad));
            Console.WriteLine("Producto agregado exitosamente.");
        }

        static void EliminarProducto()
        {
            Console.Write("Nombre del producto a eliminar: ");
            var nombre = Console.ReadLine();
            inventario.EliminarProducto(nombre);
            Console.WriteLine("Producto eliminado exitosamente.");
        }

        static void ActualizarProducto()
        {
            Console.Write("Nombre del producto a actualizar: ");
            var nombre = Console.ReadLine();
            Console.Write("Nueva cantidad: ");
            var nuevaCantidad = int.Parse(Console.ReadLine());

            inventario.ActualizarProducto(nombre, nuevaCantidad);
            Console.WriteLine("Producto actualizado exitosamente.");
        }

        static void RealizarCompra()
        {
            var carrito = new CarritoCompra();
            while (true)
            {
                Console.Write("Nombre del producto para comprar (o 'salir' para finalizar): ");
                var nombre = Console.ReadLine();
                if (nombre.ToLower() == "salir")
                {
                    break;
                }

                var producto = inventario.ObtenerProducto(nombre);
                if (producto == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    continue;
                }

                Console.Write("Cantidad: ");
                var cantidad = int.Parse(Console.ReadLine());
                try
                {
                    carrito.AgregarProducto(producto, cantidad);
                    inventario.ActualizarProducto(nombre, producto.Cantidad - cantidad);
                    Console.WriteLine("Producto agregado al carrito.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var total = carrito.CalcularTotal();
            Console.WriteLine($"Total de la compra: {total:C}");
            var transaccion = new Transaccion(carrito.ObtenerProductos(), total);
            historial.AgregarTransaccion(transaccion);
            Console.WriteLine("Compra realizada con éxito.");
        }

        static void VerHistorial()
        {
            var transacciones = historial.ObtenerTransacciones();
            foreach (var transaccion in transacciones)
            {
                Console.WriteLine($"Total: {transaccion.Total:C}");
                foreach (var producto in transaccion.ProductosComprados)
                {
                    Console.WriteLine($"- {producto.Nombre} (x{producto.Cantidad})");
                }
            }
        }
    }
}
