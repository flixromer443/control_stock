using control_stock.config;
using control_stock.DTO;
using control_stock.services.producto_service;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock.services.producto_service
{
    internal class ProductoDAOImpl : ProductoDAO
    {
        
        private static Conexion conexion = new Conexion();

        public void create(ProductoDTO productoDTO)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();
            
            connection.Open();
            
            command.CommandText = "INSERT INTO productos(categoria_id,  descripcion, precio_compra, precio_venta, stock) VALUES (@CATEGORIA_ID, @DESCRIPCION, @PRECIO_COMPRA, @PRECIO_VENTA, @STOCK)";
            command.Parameters.Add(new SQLiteParameter("@CATEGORIA_ID", productoDTO.CategoriaId));
            command.Parameters.Add(new SQLiteParameter("@DESCRIPCION", productoDTO.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@PRECIO_COMPRA", productoDTO.PrecioCompra));
            command.Parameters.Add(new SQLiteParameter("@PRECIO_VENTA", productoDTO.PrecioVenta));
            command.Parameters.Add(new SQLiteParameter("@STOCK", productoDTO.Stock));
            command.ExecuteNonQuery();

            connection.Close();

        }

        public void delete(int productoId)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();

            connection.Open();
            command.CommandText = "DELETE FROM productos WHERE id =@ID";
            command.Parameters.Add(new SQLiteParameter("ID", productoId));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void update(ProductoDTO productoDTO)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "UPDATE productos SET categoria_id = @CATEGORIA_ID,  descripcion = @DESCRIPCION, precio_compra = @PRECIO_COMPRA, precio_venta = @PRECIO_VENTA, stock = @STOCK WHERE id = @ID ";
            command.Parameters.Add(new SQLiteParameter("@ID", productoDTO.Id));
            command.Parameters.Add(new SQLiteParameter("@CATEGORIA_ID", productoDTO.CategoriaId));
            command.Parameters.Add(new SQLiteParameter("@DESCRIPCION", productoDTO.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@PRECIO_COMPRA", productoDTO.PrecioCompra));
            command.Parameters.Add(new SQLiteParameter("@PRECIO_VENTA", productoDTO.PrecioVenta));
            command.Parameters.Add(new SQLiteParameter("@STOCK", productoDTO.Stock));
            command.ExecuteNonQuery();

            connection.Close();
        }
        public void update(List<ProductoDTO> productosDTO)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();
            for(int i = 0; i<productosDTO.Count(); i++)
            {
                ProductoDTO productoDTO = productosDTO[i];
                command.CommandText = "UPDATE productos SET categoria_id = @CATEGORIA_ID,  descripcion = @DESCRIPCION, precio_compra = @PRECIO_COMPRA, precio_venta = @PRECIO_VENTA, stock = @STOCK WHERE id = @ID ";
                command.Parameters.Add(new SQLiteParameter("@ID", productoDTO.Id));
                command.Parameters.Add(new SQLiteParameter("@CATEGORIA_ID", productoDTO.CategoriaId));
                command.Parameters.Add(new SQLiteParameter("@DESCRIPCION", productoDTO.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@PRECIO_COMPRA", productoDTO.PrecioCompra));
                command.Parameters.Add(new SQLiteParameter("@PRECIO_VENTA", productoDTO.PrecioVenta));
                command.Parameters.Add(new SQLiteParameter("@STOCK", productoDTO.Stock));
                command.ExecuteNonQuery();
            }
            connection.Close();


        }


        public void updateStockProducto(int productoId, int unidades)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "UPDATE productos SET stock = @STOCK WHERE id = @ID ";
            command.Parameters.Add(new SQLiteParameter("@ID", productoId));
            command.Parameters.Add(new SQLiteParameter("@STOCK", unidades));
            command.ExecuteNonQuery();

            connection.Close();
        }
        public int updatePreciosByCategoria(int categoriaId, int indiceDeAumento)
        {
            List<ProductoDTO> productosDTO = findByCategoriaId(categoriaId);
            for (int i = 0; i < productosDTO.Count; i++)
            {
                ProductoDTO productoDTO = productosDTO[i];
                int suma = int.Parse(productoDTO.PrecioVenta) * indiceDeAumento / 100;
                int precioAumentado = int.Parse(productoDTO.PrecioVenta) + suma;
                productoDTO.PrecioVenta = precioAumentado.ToString();
                update(productoDTO);
            }
            return productosDTO.Count();
        }
        
        public List<ProductoDTO> findByCategoriaId(int categoriaId)
        {
            List<ProductoDTO> productos = new List<ProductoDTO>();
            SQLiteConnection connection = conexion.conectar();

            SQLiteCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "SELECT * FROM productos WHERE categoria_id =@CATEGORIA_ID";
            command.Parameters.Add(new SQLiteParameter("CATEGORIA_ID", categoriaId));
            
            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                ProductoDTO productoDTO = new ProductoDTO();
                productoDTO.Id = (int)(long)dataReader[0];
                productoDTO.CategoriaId = (int)(long)dataReader[1];
                productoDTO.Descripcion = (string)dataReader[2];
                productoDTO.PrecioCompra = (string)dataReader[3];
                productoDTO.PrecioVenta = (string)dataReader[4];
                productoDTO.Stock = (int)(long)dataReader[5];
                productos.Add(productoDTO);
            } 
            connection.Close();
            return productos;
        }

        public ProductoDTO findById(int productoId)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM productos WHERE id =@ID";
            command.Parameters.Add(new SQLiteParameter("ID", productoId));

            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                ProductoDTO productoDTO = new ProductoDTO();
                productoDTO.Id = (int)(long)dataReader[0];
                productoDTO.CategoriaId = (int)(long)dataReader[1];
                productoDTO.Descripcion = (string)dataReader[2];
                productoDTO.PrecioCompra = (string)dataReader[3];
                productoDTO.PrecioVenta = (string)dataReader[4];
                productoDTO.Stock = (int)(long)dataReader[5];
                connection.Close();
                return productoDTO;
            }
            connection.Close();
            return new ProductoDTO();
        }
        
        public List<ProductoDTO> findAll()
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();
            
            connection.Open();
            command.CommandText = "SELECT * FROM productos";
            
            SQLiteDataReader dataReader = command.ExecuteReader();
            List<ProductoDTO> productos = new List<ProductoDTO>();
            while (dataReader.Read())
            {
                ProductoDTO productoDTO = new ProductoDTO();
                productoDTO.Id = (int)(long)dataReader[0];
                productoDTO.CategoriaId = (int)(long)dataReader[1];
                productoDTO.Descripcion = (string)dataReader[2];
                productoDTO.PrecioCompra = (string)dataReader[3];
                productoDTO.PrecioVenta = (string)dataReader[4];
                productoDTO.Stock = (int)(long)dataReader[5];
                productos.Add(productoDTO);
            }
            connection.Close();
            return productos;
        }

        private String productoDTOToString(ProductoDTO productoDTO)
        {
            StringBuilder producto = new StringBuilder();
            producto.Append(productoDTO.Id + "," +
                            productoDTO.CategoriaId + "," +
                            productoDTO.Descripcion + "," +
                            productoDTO.PrecioCompra + "," +
                            productoDTO.PrecioVenta + "," +
                            productoDTO.Stock);

            return producto.ToString();
        }
    }
}
