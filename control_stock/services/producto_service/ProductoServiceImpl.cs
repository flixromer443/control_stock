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
    internal class ProductoServiceImpl : ProductoService
    {
        private const string ruta = "archivos/productos.txt";
        private static string datasource = "Data Source=control_stockV01.db";
        //private static string datasource = "Data Source= C:/Users/Felix.DESKTOP-U548A3J/source/repos/control_stock/control_stock/control_stockV01.db";


        public void create(ProductoDTO productoDTO)
        {
            SQLiteConnection connection = new SQLiteConnection(datasource);
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
            SQLiteConnection connection = new SQLiteConnection(datasource);
            SQLiteCommand command = connection.CreateCommand();

            connection.Open();
            command.CommandText = "DELETE FROM productos WHERE id =@ID";
            command.Parameters.Add(new SQLiteParameter("ID", productoId));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void update(ProductoDTO productoDTO)
        {
            SQLiteConnection connection = new SQLiteConnection(datasource);
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
        public void updateStockProducto(int productoId, int unidades)
        {
            SQLiteConnection connection = new SQLiteConnection(datasource);
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
            int i = 0;

            SQLiteConnection connection = new SQLiteConnection(datasource);
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM productos WHERE categoria_id =@CATEGORIA_ID";
            command.Parameters.Add(new SQLiteParameter("CATEGORIA_ID", categoriaId));
            
            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(i))
                {
                    ProductoDTO productoDTO = new ProductoDTO();
                    productoDTO.Id = (int)(long) dataReader[0];
                    productoDTO.CategoriaId = (int)(long) dataReader[1];
                    productoDTO.Descripcion = (string) dataReader[2];
                    productoDTO.PrecioCompra = (string) dataReader[3];
                    productoDTO.PrecioVenta = (string) dataReader[4];
                    productoDTO.Stock = (int)(long) dataReader[5];
                    productos.Add(productoDTO);
                }
                i++;
            } 
            connection.Close();
            return productos;
        }

        public ProductoDTO findById(int productoId)
        {
            int i = 0;

            SQLiteConnection connection = new SQLiteConnection(datasource);
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM productos WHERE id =@ID";
            command.Parameters.Add(new SQLiteParameter("ID", productoId));

            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(i))
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
                i++;
            }
            connection.Close();
            return new ProductoDTO();
        }
        
        
        










        public List<ProductoDTO> findAll()
        {
            SQLiteConnection connection = new SQLiteConnection(datasource);
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM productos";
            SQLiteDataReader dataReader = command.ExecuteReader();

            List<ProductoDTO> productos = new List<ProductoDTO>();
            int i = 0;
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(i))
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
                i++;
            }
            connection.Close();
            return productos;


            /*try
            {
                List<ProductoDTO> productos = new List<ProductoDTO>();
                StreamReader archivo = new StreamReader(ruta);

                while (!archivo.EndOfStream)
                {
                    string linea = archivo.ReadLine();
                    string[] producto = linea.Split(",");
                    ProductoDTO productoDTO = new ProductoDTO();
                    productoDTO.Id = int.Parse(producto[0]);
                    productoDTO.CategoriaId = int.Parse(producto[1]);
                    productoDTO.Descripcion = producto[2];
                    productoDTO.PrecioCompra = producto[3];
                    productoDTO.PrecioVenta = producto[4];
                    productoDTO.Stock = int.Parse(producto[5]);

                    productos.Add(productoDTO);

                }
                archivo.Close();
                return productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return new List<ProductoDTO>();*/
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
