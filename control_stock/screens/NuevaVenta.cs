using control_stock.DTO;
using control_stock.services.producto_service;
using control_stock.util;
using control_stock.util.productos;

namespace control_stock
{
    public partial class NuevaVenta : Form
    {
        private ProductoDTO productoSeleccionado;
        private int unidadesSeleccionadas = 1;
        private GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();
        private ProductoServiceImpl productoService = new ProductoServiceImpl();
        private ProductosUtil productosUtil = new ProductosUtil();

        public ProductoDTO ProductoSeleccionado { get => productoSeleccionado; set => productoSeleccionado = value; }

        public NuevaVenta()
        {
            InitializeComponent();
        }

        private void NuevaVenta_Load(object sender, EventArgs e)
        {
            //PRODUCTO
            string descripcion = productoSeleccionado.Descripcion;
            label4.Text = (descripcion.Length > 20) ? productosUtil.reducirCaracteresDescripcion(descripcion)
                                                   : descripcion;

            //UNIDADES SELECCIONADAS
            label5.Text = unidadesSeleccionadas.ToString();

            //TOTAL UNIDADES
            label7.Text = productoSeleccionado.Stock.ToString();

            //PRECIO
            label9.Text = productosUtil.generarPrecio(productoSeleccionado.PrecioVenta);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string unidades = (unidadesSeleccionadas == 1) ? "una unidad"
                                                           : unidadesSeleccionadas.ToString() + " unidades";
            string mensaje = "Desea vender " + unidades + " de este producto?";
            DialogResult response = generadorDeMensajes.generarMensaje(mensaje, MessageBoxIcon.Question);

            if (response == DialogResult.OK)
            {
                productoService.updateStockProducto(productoSeleccionado.Id, unidadesSeleccionadas);
                generadorDeMensajes.generarMensaje(Mensajes.PRODUCTO_VENDIDO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (unidadesSeleccionadas < productoSeleccionado.Stock)
            {
                unidadesSeleccionadas += 1;
                label9.Text = productosUtil.generarPrecio(multiplicarPrecioPorUnidad());
                label5.Text = unidadesSeleccionadas.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (unidadesSeleccionadas > 1)
            {
                unidadesSeleccionadas -= 1;
                label9.Text = productosUtil.generarPrecio(multiplicarPrecioPorUnidad());
                label5.Text = unidadesSeleccionadas.ToString();
            }
        }

        private string multiplicarPrecioPorUnidad()
        {
            int precio = int.Parse(productoSeleccionado.PrecioVenta) * unidadesSeleccionadas;
            return precio.ToString();
        }


    }
}
