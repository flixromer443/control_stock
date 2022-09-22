using control_stock.DTO;
using control_stock.services.producto_service;
using control_stock.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock
{
    public partial class ActualizarProducto : Form
    {
        private ProductoDTO productoSeleccionado;
        private ProductoServiceImpl productoService = new ProductoServiceImpl();
        private GenerardorDeMensajes generadorDeMensajes = new GenerardorDeMensajes();
        private ProductosUtil productosUtil = new ProductosUtil();

        public ActualizarProducto()
        {
            InitializeComponent();
        }

        public ProductoDTO ProductoSeleccionado { get => productoSeleccionado; set => productoSeleccionado = value; }

        private void ActualizarProducto_Load(object sender, EventArgs e)
        {
            textBox1.Text = productoSeleccionado.Descripcion;
            textBox2.Text = productoSeleccionado.PrecioCompra;
            textBox3.Text = productoSeleccionado.PrecioVenta;
            textBox4.Text = productoSeleccionado.Stock.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductoDTO productoActualizado = new ProductoDTO();
            productoActualizado.Id = productoSeleccionado.Id;
            productoActualizado.CategoriaId = productoSeleccionado.CategoriaId;
            productoActualizado.Descripcion = textBox1.Text;
            productoActualizado.PrecioCompra = textBox2.Text;
            productoActualizado.PrecioVenta = textBox3.Text;
            productoActualizado.Stock = (textBox4.Text!="") ? int.Parse(textBox4.Text) : 0;

            Boolean error = Validador.validarProducto(productoActualizado);

            if (!error)
            {
                DialogResult response = generadorDeMensajes.generarMensaje(Mensajes.ACTUALIZAR_PRODUCTO, MessageBoxIcon.Question);
                
                if (response == DialogResult.OK)
                {
                    productoService.update(productoActualizado);
                    generadorDeMensajes.generarMensaje(Mensajes.PRODUCTO_ACTUALIZADO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

    }
}
