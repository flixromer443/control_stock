using control_stock.DAO.ventas;
using control_stock.DTO;
using control_stock.services.producto_service;
using control_stock.util;
using control_stock.util.carrito;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock
{
    public partial class Carrito : Form
    {
        List<ProductoDTO> productos = new List<ProductoDTO>();
        private GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();
        private ProductoDAOImpl productoService = new ProductoDAOImpl();
        private VentaDAOImpl ventasService = new VentaDAOImpl();
        private CarritoUtil carritoUtil = new CarritoUtil();
        private int total;
        public Carrito()
        {
            InitializeComponent();
            autocompletar();
        }

        public List<ProductoDTO> Productos { get => productos; set => productos = value; }
        public int Total { get => total; set => total = value; }

        private void button2_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            for(int i=0; i< productos.Count(); i++)
            {
                ProductoDTO producto = productos[i];
                if(productos[i].Descripcion.ToString().Trim() == texto.Trim())
                {
                    //borro el producto por el id y devuelvo las unidades que haya en el listview
                    int unidades = carritoUtil.eliminarElementoYDevolverUnidades(producto.Id.ToString(), listView1);
                        
                    Boolean existeProducto = carritoUtil.verficarSiExisteProducto(producto.Id.ToString(), listView1);
                    Boolean unidadesValidas = carritoUtil.validarUnidadesExistentes(producto.Stock, unidades);
                    
                    //valido que haya stock
                    unidades = (unidadesValidas) ? unidades + 1 : unidades;
                   
                    ListViewItem fila = new ListViewItem(producto.Id.ToString());
                    fila.BackColor = Color.WhiteSmoke;
                    fila.SubItems.Add(producto.Descripcion);
                    fila.SubItems.Add(unidades.ToString());

                    _ = (existeProducto) ? fila.SubItems.Add(unidades.ToString())
                                         : fila.SubItems.Add("1");

                    listView1.Items.Add(fila);

                    _ = (unidadesValidas) ? total += carritoUtil.obtenerPrecioProducto(producto.Id, productos) 
                                          : carritoUtil.mostrarErrorUnidades();

                    label4.Text = "$" + total.ToString();
                    
                }
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            total = 0;
            label4.Text = "$0";
            listView1.Items.Clear();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {

            if (total != 0)
            {
                DialogResult response = generadorDeMensajes.generarMensaje(Mensajes.VENDER_CARRITO, MessageBoxIcon.Warning);

                if (response == DialogResult.OK)
                {
                    productoService.update(carritoUtil.cargarListaParaActualizarStock(listView1, Productos));
                    ventasService.cargarVentas(carritoUtil.cargarListaParaCargarVentas(listView1, Productos));
                    generadorDeMensajes.generarMensaje(Mensajes.CARRITO_VENDIDO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }    
        }

       

        //carga el texbox con todos los productos que cuenten con stock
        private void autocompletar()
        {

            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            productos = productoService.findAll();
            for (int i = 0; i < productos.Count; i++)
            {
                ProductoDTO producto = productos[i];
                if (producto.Stock > 0)
                {
                    lista.Add(producto.Descripcion.ToString());
                }
            }
            textBox1.AutoCompleteCustomSource = lista;
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

    }
}
