using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TiendaDeRegalo.Clases;

namespace TiendaDeRegalosTest
{
    [TestClass]
    public class InventarioTests
    {
        private Inventario inventario;
        private HistorialTransacciones historial;

        [TestInitialize]
        public void Setup()
        {
            inventario = new Inventario();
            historial = new HistorialTransacciones();
        }

        [TestMethod]
        public void AgregarProducto_ProductoNuevo_ProductoDisponible()
        {
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            var productoEncontrado = inventario.ObtenerProducto("Regalo1");
            Assert.IsNotNull(productoEncontrado);
            Assert.AreEqual("Regalo1", productoEncontrado.Nombre);
        }

        [TestMethod]
        public void EliminarProducto_ProductoExistente_ProductoNoDisponible()
        {
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            inventario.EliminarProducto("Regalo1");
            var productoEncontrado = inventario.ObtenerProducto("Regalo1");
            Assert.IsNull(productoEncontrado);
        }

        [TestMethod]
        public void ActualizarProducto_CantidadValida_CantidadActualizada()
        {
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            inventario.ActualizarProducto("Regalo1", 10);
            var productoEncontrado = inventario.ObtenerProducto("Regalo1");
            Assert.AreEqual(10, productoEncontrado.Cantidad);
        }

        [TestMethod]
        public void RealizarCompra_CantidadValida_CantidadDescontada()
        {
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            var carrito = new CarritoCompra();
            carrito.AgregarProducto(producto, 3);

            inventario.ActualizarProducto("Regalo1", producto.Cantidad - 3);
            Assert.AreEqual(2, inventario.ObtenerProducto("Regalo1").Cantidad);
        }

        [TestMethod]
        public void HistorialTransacciones_AgregarTransaccion_TransaccionGuardada()
        {
            var carrito = new CarritoCompra();
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            carrito.AgregarProducto(producto, 2);
            var total = carrito.CalcularTotal();
            var transaccion = new Transaccion(carrito.ObtenerProductos(), total);

            historial.AgregarTransaccion(transaccion);
            var transacciones = historial.ObtenerTransacciones();
            Assert.AreEqual(1, transacciones.Count);
        }

        [TestMethod]
        public void ComprarProductoNoDisponible_LanzaExcepcion()
        {
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var carrito = new CarritoCompra();
                carrito.AgregarProducto(producto, 6); 
            });
        }

        [TestMethod]
        public void ComprarCantidadExcedente_LanzaExcepcion()
        {
            var producto = new Producto("Regalo1", 10.00m, 5);
            inventario.AgregarProducto(producto);

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var carrito = new CarritoCompra();
                carrito.AgregarProducto(producto, 6); 
            });
        }
    }
}

