using control_stock.config;
using control_stock.DTO;
using control_stock.services.producto_service;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.DAO.ventas
{
    internal class VentasDAOImpl
    {
        private static Conexion conexion = new Conexion();

        public void cargarVentas(List<VentaDTO> ventasDTO)
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();

            for (int i=0; i<ventasDTO.Count();i++)
            {
                VentaDTO ventaDTO = ventasDTO[i];
                connection.Open();

                command.CommandText = "INSERT INTO ventas(producto_id,  descripcion, precio_venta, unidades_vendidas, ganancia, fecha, hora) VALUES (@PRODUCTO_ID, @DESCRIPCION, @PRECIO_VENTA, @UNIDADES_VENDIDAS, @GANANCIA, @FECHA, @HORA)";
                command.Parameters.Add(new SQLiteParameter("@PRODUCTO_ID", ventaDTO.Producto_id));
                command.Parameters.Add(new SQLiteParameter("@DESCRIPCION", ventaDTO.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@PRECIO_VENTA", ventaDTO.PrecioVenta));
                command.Parameters.Add(new SQLiteParameter("@UNIDADES_VENDIDAS", ventaDTO.Unidades_vendidas));
                command.Parameters.Add(new SQLiteParameter("@GANANCIA", ventaDTO.Ganancia));
                command.Parameters.Add(new SQLiteParameter("@FECHA", ventaDTO.Fecha));
                command.Parameters.Add(new SQLiteParameter("@HORA", ventaDTO.Hora));

                command.ExecuteNonQuery();

                connection.Close();
            }
            findAll();

        }
        private void findAll()
        {
            SQLiteConnection connection = conexion.conectar();
            SQLiteCommand command = connection.CreateCommand();

            connection.Open();
            command.CommandText = "SELECT * FROM ventas";

            SQLiteDataReader dataReader = command.ExecuteReader();
            List<VentaDTO> ventas = new List<VentaDTO>();
            while (dataReader.Read())
            {
                VentaDTO ventaDTO = new VentaDTO();
                ventaDTO.Id = (int)(long)dataReader[0];
                ventaDTO.Producto_id= (int)(long)dataReader[1];
                ventaDTO.Descripcion = (string)dataReader[2];
                ventaDTO.PrecioVenta = (string)dataReader[3];

                ventaDTO.Unidades_vendidas = (int)(long)dataReader[4];
                ventaDTO.Ganancia = (string)dataReader[5];
                ventaDTO.Fecha = (string)dataReader[6];
                ventaDTO.Hora = (string)dataReader[7];

                ventas.Add(ventaDTO);
            }
            connection.Close();
        }
    }
}
