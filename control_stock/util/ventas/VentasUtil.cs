using control_stock.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util.ventas
{
    internal class VentasUtil
    {
        public string reducirCaracteresVenta(string descripcion)
        {
            return descripcion.Substring(0, 25) + ".";
        }
        public string calcularGanancias(List<VentaDTO> ventas)
        {
            int total = 0;
            for(int i=0; i < ventas.Count; i++)
            {
                int ganancia = int.Parse(ventas[i].Ganancia);
                total += ganancia;
            }
            return total.ToString();
        }
    }
}
