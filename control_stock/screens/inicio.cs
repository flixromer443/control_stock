using control_stock.DTO;
using control_stock.screens;

namespace control_stock
{
    public partial class inicio : Form
    {

        public inicio()
        {
            InitializeComponent();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           categoria form2 = new categoria();
           form2.CategoriaId = 1;
           form2.CategoriaNombre = "GOL. Y SNACKS";
           form2.ShowDialog();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            categoria form2 = new categoria();
            form2.CategoriaId = 2;
            form2.CategoriaNombre = "BEBIDAS";
            form2.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            categoria form2 = new categoria();
            form2.CategoriaId = 3;
            form2.CategoriaNombre = "CIGARRILLOS";
            form2.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            categoria form2 = new categoria();
            form2.CategoriaId = 4;
            form2.CategoriaNombre = "LIBRERIA";
            form2.ShowDialog();

        }

        private void abrirCarritoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Carrito carrito = new Carrito();
            carrito.ShowDialog();
        }

        private void historialDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Historial historial = new Historial();
            historial.ShowDialog();
        }
    }
}