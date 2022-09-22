using control_stock.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.services.producto_service
{
    internal interface ArchivoService
    {
        public List<string> extraerProductosPorCategoria(int productoId);
        public List<string> extraerProductos();
        
    }
}
