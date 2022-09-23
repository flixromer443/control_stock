using control_stock.DTO;
using control_stock.services.producto_service;
using control_stock.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock
{
    public partial class SubirPrecios : Form
    {
        private int indiceDeAumento = 0;
        private int categoriaId;

        private ProductoServiceImpl productoService = new ProductoServiceImpl();
        public SubirPrecios()
        {
            InitializeComponent();
        }

        public int IndiceDeAumento { get => indiceDeAumento; set => indiceDeAumento = value; }
        public int CategoriaId { get => categoriaId; set => categoriaId = value; }

        private void button2_Click(object sender, EventArgs e)
        {
            indiceDeAumento += 1;
            label5.Text = indiceDeAumento.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (indiceDeAumento >0)
            {
                indiceDeAumento -= 1;
                label5.Text = indiceDeAumento.ToString();

            }
        }

        private void SubirPrecios_Load(object sender, EventArgs e)
        {
            label5.Text = indiceDeAumento.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(indiceDeAumento > 0)
            {
                GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();
                string mensaje = "Desea aumentar un " + indiceDeAumento + "% el precio de venta de todos los productos de esta categoria?";
                DialogResult response = generadorDeMensajes.generarMensaje(mensaje, MessageBoxIcon.Warning);
                if (response == DialogResult.OK)
                {
                    int productosAfectados = productoService.updatePreciosByCategoria(categoriaId, indiceDeAumento);
                    string mensajeProductosActualizados = (productosAfectados == 1) ? "Operacion exitosa, un producto fue actualizado."
                                                                                    : "Operacion exitosa, " + productosAfectados.ToString() + " productos fueron actualizados.";
                    generadorDeMensajes.generarMensaje(mensajeProductosActualizados, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
