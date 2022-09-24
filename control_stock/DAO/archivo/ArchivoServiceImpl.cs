using control_stock.DTO;
using control_stock.services.producto_service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock.services.producto_service
{
    internal class ArchivoServiceImpl : ArchivoService
    {
        private const string ruta = "archivos/productos.txt";

        //DEVUELVE UNA LISTA CON TODOS LOS PRODUCTOS EXCEPTO EL REFERENCIADO EN FORMATO STRING
        public List<string> extraerProductosPorCategoria(int categoriaId)
        {
            ProductoDAOImpl productoService = new ProductoDAOImpl();
            List<ProductoDTO> productosDTO = productoService.findAll();
            List<string> productos = new List<string>();

            for (int i = 0; i < productosDTO.Count; i++)
            {
                ProductoDTO productoDTO = productosDTO[i];
                if (productoDTO.CategoriaId != categoriaId)
                {
                    StringBuilder producto = new StringBuilder();
                    producto.Append(productoDTO.Id + "," +
                                    productoDTO.CategoriaId + "," +
                                    productoDTO.Descripcion + "," +
                                    productoDTO.PrecioCompra + "," +
                                    productoDTO.PrecioVenta + "," +
                                    productoDTO.Stock);

                    productos.Add(producto.ToString());
                }


            }
            return productos;
        }

        //DEVUELVE UNA LISTA CON TODOS LOS PRODUCTOS EN FORMATO STRING
        public List<string> extraerProductos()
        {
            ProductoDAOImpl productoService = new ProductoDAOImpl();
            List<ProductoDTO> productosDTO = productoService.findAll();
            List<string> productos = new List<string>();

            for (int i = 0; i < productosDTO.Count; i++)
            {
                ProductoDTO productoDTO = productosDTO[i];
                StringBuilder producto = new StringBuilder();
                producto.Append(productoDTO.Id + "," +
                                productoDTO.CategoriaId + "," +
                                productoDTO.Descripcion + "," +
                                productoDTO.PrecioCompra + "," +
                                productoDTO.PrecioVenta + "," +
                                productoDTO.Stock);

                productos.Add(producto.ToString());

            }
            return productos;
        }

        //DEVUELVE UNA LISTA CON TODOS LOS PRODUCTOS EXCEPTO EL REFERENCIADO EN FORMATO STRING
        public List<string> extraerProductoPorId(int productoId)
        {
            ProductoDAOImpl productoService = new ProductoDAOImpl();
            List<ProductoDTO> productosDTO = productoService.findAll();
            List<string> productos = new List<string>();

            for (int i = 0; i < productosDTO.Count; i++)
            {
                ProductoDTO productoDTO = productosDTO[i];
                if (!productoId.Equals(productoDTO.Id))
                {
                    StringBuilder producto = new StringBuilder();
                    producto.Append(productoDTO.Id + "," +
                                    productoDTO.CategoriaId + "," +
                                    productoDTO.Descripcion + "," +
                                    productoDTO.PrecioCompra + "," +
                                    productoDTO.PrecioVenta + "," +
                                    productoDTO.Stock);

                    productos.Add(producto.ToString());
                }


            }
            return productos;
        }

    }
}
