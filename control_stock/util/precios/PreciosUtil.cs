using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util.precios
{
    internal class PreciosUtil
    {
        public static string generarPrecio(string precio)
        {
            StringBuilder precioVenta = new StringBuilder();
            precioVenta.Append("$" + precio);
            return precioVenta.ToString();
        }
    }
}
