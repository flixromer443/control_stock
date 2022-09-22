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
    public partial class Carrito : Form
    {
        List<ProductoDTO> productos = new List<ProductoDTO>();
        private GenerardorDeMensajes generadorDeMensajes = new GenerardorDeMensajes();

        private ProductoServiceImpl productoService = new ProductoServiceImpl();
        private int total;
        public Carrito()
        {
            InitializeComponent();
            autocompletar();
        }

        public List<ProductoDTO> Productos { get => productos; set => productos = value; }
        public int Total { get => total; set => total = value; }

        private void Carrito_Load(object sender, EventArgs e)
        {
            
        }
        private void autocompletar()
        {

            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            productos = productoService.findAll();
            for (int i = 0; i < productos.Count; i++)
            {
                ProductoDTO producto = productos[i];
                lista.Add(producto.Descripcion.ToString());
            } 
            textBox1.AutoCompleteCustomSource = lista;
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            for(int i=0; i< productos.Count(); i++)
            {
                ProductoDTO producto = productos[i];
                if(productos[i].Descripcion.ToString() == texto)
                {

                    Boolean response = verficarSiExisteProducto(producto.Id.ToString());
                    if (response)
                    {
                        
                        int unidades = eliminarElemento(producto.Id.ToString()) + 1;
                        ListViewItem fila = new ListViewItem(producto.Id.ToString());
                        fila.SubItems.Add(producto.Descripcion);
                        fila.SubItems.Add(unidades.ToString());
                        listView1.Items.Add(fila);
                    }
                    else
                    {
                        ListViewItem fila = new ListViewItem(producto.Id.ToString());
                        fila.SubItems.Add(producto.Descripcion);
                        fila.SubItems.Add("1");
                        listView1.Items.Add(fila);
                    }
                    total += obtenerPrecioProducto(producto.Id);
                    label4.Text = total.ToString();
                }
            }
            

        }
        private int eliminarElemento(string productoId) 
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
        private Boolean verficarSiExisteProducto(string productoId)
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
        private int obtenerPrecioProducto(int productoId)
        {
            for(int i=0; i < productos.Count(); i++)
            {
                ProductoDTO producto = productos[i];
                if(producto.Id == productoId)
                {
                    return int.Parse(producto.PrecioVenta);
                }
            }
            return 0;

        }
        private string convertToDinero(int dinero)
        {
            string plata = dinero.ToString();
            StringBuilder efectivo = new StringBuilder();
            for(int i=0; i<plata.Length; i++)
            {
                efectivo.Append(plata[i]);
                if (efectivo.Length == 3 || efectivo.Length == 7 || efectivo.Length == 11)
                {
                    efectivo.Append(".");
                }
            }
            plata = "$" + efectivo.ToString();
            return plata;
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
                string mensaje = "Desea confirmar esta venta?";
                DialogResult response = generadorDeMensajes.generarMensaje(mensaje, MessageBoxIcon.Warning);

                if (response == DialogResult.OK)
                {
                    generadorDeMensajes.generarMensaje("Operacion exitosa, se ha actualizado el stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }    
        }
    }
}
