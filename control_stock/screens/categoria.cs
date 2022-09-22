using control_stock.DTO;
using control_stock.services.producto_service;
using control_stock.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock
{
    public partial class categoria : Form
    {
        private int categoriaId;
        private string categoriaNombre;
        
        private ProductoDTO productoSeleccionado = new ProductoDTO();
        private GenerardorDeMensajes generadorDeMensajes = new GenerardorDeMensajes();
        private ProductosUtil productosUtil = new ProductosUtil();

        private readonly ProductoServiceImpl productoService = new ProductoServiceImpl();

        public categoria()
        {
            InitializeComponent();
        }
        public int CategoriaId { get => categoriaId; set => categoriaId = value; }
        public string CategoriaNombre { get => categoriaNombre; set => categoriaNombre = value; }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = CategoriaNombre;
            List<ProductoDTO> productos = productoService.findByCategoriaId(CategoriaId);
            for(int i = 0; i < productos.Count; i++)
            {
                ProductoDTO producto = productos[i];
                dataGridView1.Rows.Add(producto.Id,
                                       producto.Descripcion,
                                       productosUtil.generarPrecio(producto.PrecioCompra.ToString()),
                                       productosUtil.generarPrecio(producto.PrecioVenta.ToString()),
                                       producto.Stock);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean error = productosUtil.validarProductoSeleccionado(productoSeleccionado);
            if (!error)
            {
                ActualizarProducto actualizarProducto = new ActualizarProducto();
                actualizarProducto.ProductoSeleccionado = productoSeleccionado;
                actualizarProducto.ShowDialog();
                refrescarDataGridView(productoService.findByCategoriaId(CategoriaId));
                productoSeleccionado = new ProductoDTO();
            }
            else
            {
                generadorDeMensajes.generarMensaje(Mensajes.SELECCIONAR_PRODUCTO, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarProducto agregarProducto = new AgregarProducto();
            agregarProducto.CategoriaId = categoriaId;
            agregarProducto.ShowDialog();
            dataGridView1.Rows.Clear();
            refrescarDataGridView(productoService.findByCategoriaId(CategoriaId));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoDTO producto = productoSeleccionado;

                if(producto.Descripcion!= null)
                {
                    string mensaje = "Desea quitar el producto '" + producto.Descripcion.ToUpper() + "' de la lista?";
                    DialogResult response = generadorDeMensajes.generarMensaje(mensaje, MessageBoxIcon.Warning);
                    
                    if(response == DialogResult.OK)
                    {
                        productoService.delete(producto.Id);
                        productoSeleccionado = new ProductoDTO();
                        generadorDeMensajes.generarMensaje(Mensajes.PRODUCTO_ELIMINADO,MessageBoxButtons.OK,MessageBoxIcon.Information);
                        refrescarDataGridView(productoService.findByCategoriaId(CategoriaId));
                    }
                }
                else
                {
                    generadorDeMensajes.generarMensaje(Mensajes.SELECCIONAR_PRODUCTO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch
            {
                generadorDeMensajes.generarError("Error, el codigo de producto se encuentra vacio o no es valido");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Boolean error = productosUtil.validarProductoSeleccionado(productoSeleccionado);

            if (!error)
            {
                if(productoSeleccionado.Stock>0)
                {
                    NuevaVenta nuevaVenta = new NuevaVenta();
                    nuevaVenta.ProductoSeleccionado = productoSeleccionado;
                    nuevaVenta.ShowDialog();
                    productoSeleccionado = new ProductoDTO();
                    refrescarDataGridView(productoService.findByCategoriaId(CategoriaId));
                }
                else
                {
                    generadorDeMensajes.generarError(Mensajes.PRODUCTO_SIN_STOCK);
                }
                
            }
            else
            {
                generadorDeMensajes.generarMensaje(Mensajes.SELECCIONAR_PRODUCTO, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                productoSeleccionado.Id = int.Parse(row.Cells[0].Value.ToString());
                productoSeleccionado.CategoriaId = categoriaId;
                productoSeleccionado.Descripcion = row.Cells[1].Value.ToString();

                string precioCompra = row.Cells[2].Value.ToString();
                string precioVenta = row.Cells[3].Value.ToString();

                productoSeleccionado.PrecioCompra = precioCompra.Substring(1, precioCompra.Length - 1);
                productoSeleccionado.PrecioVenta = precioVenta.Substring(1, precioVenta.Length - 1);

                productoSeleccionado.Stock = int.Parse(row.Cells[4].Value.ToString());

            }
        }
        private void refrescarDataGridView(List<ProductoDTO> productosDGW)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < productosDGW.Count; i++)
            {
                ProductoDTO productoDGW = productosDGW[i];
                dataGridView1.Rows.Add(productoDGW.Id,
                                       productoDGW.Descripcion,
                                       productosUtil.generarPrecio(productoDGW.PrecioCompra.ToString()),
                                       productosUtil.generarPrecio(productoDGW.PrecioVenta.ToString()),
                                       productoDGW.Stock);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<ProductoDTO> productos = productoService.findByCategoriaId(categoriaId);
            if(productos.Count != 0)
            {
                SubirPrecios subirPrecios = new SubirPrecios();
                subirPrecios.CategoriaId = categoriaId;
                subirPrecios.ShowDialog();
                refrescarDataGridView(productoService.findByCategoriaId(CategoriaId));

            }
            else
            {
                generadorDeMensajes.generarMensaje(Mensajes.CATEGORIA_VACIA, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void abrirCarritoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Carrito carrito = new Carrito();
            carrito.ShowDialog();
            refrescarDataGridView(productoService.findByCategoriaId(CategoriaId));
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
