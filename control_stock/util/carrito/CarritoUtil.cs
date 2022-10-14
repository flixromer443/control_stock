using control_stock.DTO;
using control_stock.util.fecha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util.carrito
{
    internal class CarritoUtil
    {
        private GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();

        public int eliminarElementoYDevolverUnidades(string productoId, ListView listView1)
        {

            for (int x = 0; x < listView1.Items.Count; x++)
            {
                if (listView1.Items[x].SubItems[0].Text.Trim() == productoId.Trim())
                {
                    int stock = int.Parse(listView1.Items[x].SubItems[2].Text);
                    listView1.Items[x].Remove();
                    return stock;
                }

            }
            return 0;
        }
        //verifica si el producto se encuentra en el listview
        public Boolean verficarSiExisteProducto(string productoId, ListView listView1)
        {
            for (int x = 0; x < listView1.Items.Count; x++)
            {
                if (listView1.Items[x].SubItems[0].Text.Trim() == productoId.Trim())
                {
                    return true;
                }

            }
            return false;
        }

        public int obtenerPrecioProducto(int productoId, List<ProductoDTO> productos)
        {
            for (int i = 0; i < productos.Count(); i++)
            {
                ProductoDTO producto = productos[i];
                if (producto.Id == productoId)
                {
                    return int.Parse(producto.PrecioVenta);
                }
            }
            return 0;

        }

        public int mostrarErrorUnidades()
        {
            generadorDeMensajes.generarMensaje(Mensajes.ERROR_UNIDADES, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 0;
        }

        public Boolean validarUnidadesExistentes(int stock, int unidades)
        {
            if (stock - unidades == 0) { return false; }
            return true;
        }
         private string calcularGanancia(string precioVenta, string precioCompra, int stock)
        {

            int primerMiembro = int.Parse(precioVenta) * stock;
            int segundoMiembro = int.Parse(precioCompra) * stock;
            int resultado = primerMiembro - segundoMiembro;
            
            return resultado.ToString();
        }

        public List<ProductoDTO> cargarListaParaActualizarStock(ListView listView1, List<ProductoDTO> productos)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();
            for (int x = 0; x < listView1.Items.Count; x++)
            {
                ListViewItem item = listView1.Items[x];
                for (int i = 0; i < productos.Count(); i++)
                {
                    ProductoDTO producto = productos[i];
                    if (item.SubItems[0].Text.Trim() == producto.Id.ToString().Trim())
                    {
                        producto.Stock = producto.Stock - int.Parse(item.SubItems[2].Text);
                        lista.Add(producto);
                    }
                }

            }
            return lista;
        }
        public List<VentaDTO> cargarListaParaCargarVentas(ListView listView1, List<ProductoDTO> productosDTO)
        {
            string[] fechaYhora = FechaUtil.obtenerFechaYHoraActual();
            List<VentaDTO> lista = new List<VentaDTO>();
            for (int x = 0; x < listView1.Items.Count; x++)
            {
                ListViewItem item = listView1.Items[x];
                for (int i = 0; i < productosDTO.Count(); i++)
                {
                    ProductoDTO productoDTO = productosDTO[i];
                    if (item.SubItems[0].Text.Trim() == productoDTO.Id.ToString().Trim())
                    {
                        VentaDTO ventaDTO = new VentaDTO();
                        productoDTO.Stock = productoDTO.Stock - int.Parse(item.SubItems[2].Text);

                        ventaDTO.Producto_id = productoDTO.Id;
                        ventaDTO.Descripcion = productoDTO.Descripcion;
                        ventaDTO.PrecioVenta = productoDTO.PrecioVenta;
                        ventaDTO.Unidades_vendidas = int.Parse(item.SubItems[2].Text);
                        ventaDTO.Ganancia = calcularGanancia(productoDTO.PrecioVenta, productoDTO.PrecioCompra, ventaDTO.Unidades_vendidas);
                        ventaDTO.Fecha = fechaYhora[0];
                        ventaDTO.Hora = fechaYhora[1];
                        lista.Add(ventaDTO);

                    }
                }

            }
            return lista;
        }
        //quita producto del listview
        public int quitarProductoYActualizarTotal(ListView listView1, List<ProductoDTO> productos, int total)
        {
            int indice = listView1.SelectedItems[0].Index;

            int productoId = int.Parse(listView1.Items[indice].SubItems[0].Text);
            int productoUnidades = int.Parse(listView1.Items[indice].SubItems[2].Text);

            for (int i = 0; i < productos.Count; i++)
            {
                ProductoDTO producto = productos[i];
                if (producto.Id == productoId)
                {
                    listView1.Items[indice].Remove();
                    total -= int.Parse(producto.PrecioVenta) * productoUnidades;
                    return total;
                }
            }
            return 0;
        }
    }
}
