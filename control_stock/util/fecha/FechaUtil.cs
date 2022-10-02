using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util.fecha
{
    internal class FechaUtil
    {
        public static string[] obtenerFechaYHoraActual()
        {
            DateTime fecha = DateTime.Now;
            string fechaString = fecha.ToString();
            return fechaString.Split(" ");
        }
        public static string obtenerFechaAcual()
        {
            DateTime fecha = DateTime.Now;
            string fechaString = fecha.ToString();
            string[] fechaYHora = fechaString.Split(" ");
            return fechaYHora[0];
        }

    }
}
