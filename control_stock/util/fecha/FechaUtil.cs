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
        public static string completarConCeros(string fecha)
        {
            StringBuilder nuevaFecha = new StringBuilder();
            string[] mesDiayAnio = fecha.Split("/");
            for(int i = 0; i < 3; i++)
            {
                _ = (mesDiayAnio[i].Length < 2) ? nuevaFecha.Append("0" + mesDiayAnio[i])
                                                : nuevaFecha.Append(mesDiayAnio[i]);
                if(i<2) nuevaFecha.Append("/");
            }
            return nuevaFecha.ToString();
        }

        public static string quitarCeros(string fecha)
        {
            StringBuilder nuevaFecha = new StringBuilder();
            string[] diaMesyAnio = fecha.Split("/");
            for (int i = 0; i < 3; i++)
            {
                _ = (diaMesyAnio[i].Length == 2 && int.Parse(diaMesyAnio[i]) < 10) ? nuevaFecha.Append(diaMesyAnio[i][1])
                                                                                   : nuevaFecha.Append(diaMesyAnio[i]);
                if (i < 2) nuevaFecha.Append("/");
            }
            return nuevaFecha.ToString();
        }
        public static Boolean validarFecha(string fecha)
        {
            try
            {
                string[] diaMesyAnio = fecha.Split("/");
                if (fecha.Length != 10) return false;
                if (diaMesyAnio.Length != 3) return false;
                if (diaMesyAnio[0].Length < 2) return false;
                if (diaMesyAnio[1].Length < 2) return false;
                if (diaMesyAnio[2].Length < 4) return false;
                if (int.Parse(diaMesyAnio[0]) < 1 || int.Parse(diaMesyAnio[0]) > 31) return false;
                if (int.Parse(diaMesyAnio[1]) < 1 || int.Parse(diaMesyAnio[1]) > 12) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
