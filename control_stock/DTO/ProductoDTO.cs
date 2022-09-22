using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.DTO
{
    
    public class ProductoDTO
    {
        private int id;
        private int categoriaId;
        private string descripcion;
        private string precioCompra;
        private string precioVenta;
        private int stock;

        public ProductoDTO()
        {
        }

        public ProductoDTO(int id,int categoriaId, string descripcion, string precioCompra, string precioVenta, int stock)
        {
            this.id = id;
            this.categoriaId = categoriaId;
            this.descripcion = descripcion;
            this.precioCompra = precioCompra;
            this.precioVenta = precioVenta;
            this.stock = stock;
        }

        public int Id { get => id; set => id = value; }
        public int CategoriaId { get => categoriaId; set => categoriaId = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string PrecioCompra { get => precioCompra; set => precioCompra = value; }
        public string PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public int Stock { get => stock; set => stock = value; }
    }
}
