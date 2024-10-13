using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaDeRegalo.Clases
{
    public class HistorialTransacciones
    {
        private List<Transaccion> transacciones = new List<Transaccion>();

        public void AgregarTransaccion(Transaccion transaccion)
        {
            transacciones.Add(transaccion);
        }

        public List<Transaccion> ObtenerTransacciones()
        {
            return transacciones;
        }
    }
}
