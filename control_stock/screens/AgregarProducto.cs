using control_stock.DTO;
using control_stock.services.producto_service;
using control_stock.util;
using control_stock.util.productos;
using System;
using System.Collections;
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
    public partial class AgregarProducto : Form
    {
        private int categoriaId;
        private ProductoDAOImpl productoService = new ProductoDAOImpl();
        private GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();
        private ProductosUtil productosUtil = new ProductosUtil();

        public AgregarProducto()
        {
            InitializeComponent();
        }

        public int CategoriaId { get => categoriaId; set => categoriaId = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductoDTO nuevoProducto = new ProductoDTO();
            nuevoProducto.CategoriaId = categoriaId;
            nuevoProducto.Descripcion = textBox1.Text;
            nuevoProducto.PrecioCompra = textBox2.Text;
            nuevoProducto.PrecioVenta = textBox3.Text;
            nuevoProducto.Stock = (textBox4.Text != "") ? int.Parse(textBox4.Text) : 0;

            Boolean error = Validador.validarProducto(nuevoProducto);

            if (!error)
            {
                DialogResult response = generadorDeMensajes.generarMensaje(Mensajes.AGREGAR_PRODUCTO,MessageBoxIcon.Question);
                
                if(response == DialogResult.OK)
                {
                    productoService.create(nuevoProducto);
                    generadorDeMensajes.generarMensaje(Mensajes.PRODUCTO_AGREGADO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
