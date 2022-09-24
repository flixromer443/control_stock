using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.DTO
{
    internal class VentaDTO
    {
        private int id;
        private int producto_id;
        private string descripcion;
        private string precioVenta;
        private int unidades_vendidas;
        private string ganancia;
        private string fecha;
        private string hora;

        public VentaDTO()
        {

        }

        public int Id { get => id; set => id = value; }
        public int Producto_id { get => producto_id; set => producto_id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public int Unidades_vendidas { get => unidades_vendidas; set => unidades_vendidas = value; }
        public string Ganancia { get => ganancia; set => ganancia = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
    }
}
