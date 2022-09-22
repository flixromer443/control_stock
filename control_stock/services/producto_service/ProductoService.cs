using control_stock.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.services.producto_service
{
    internal interface ProductoService
    {
        public void create(ProductoDTO producto);
        public List<ProductoDTO> findByCategoriaId(int categoriaId);
        public List<ProductoDTO> findAll();
    }
}
