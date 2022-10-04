using control_stock.DAO.ventas;
using control_stock.DTO;
using control_stock.util;
using control_stock.util.fecha;
using control_stock.util.precios;
using control_stock.util.ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock.screens
{
    public partial class Historial : Form
    {
        GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();
        VentaDAOImpl ventasService =  new VentaDAOImpl();

        //se usa para todo
        private List<VentaDTO> ventas = new List<VentaDTO>();


        //IMPRESION
        private VentasUtil ventasUtil = new VentasUtil();
        private int indice = 0;
        private int pagina = 1;
        private string[] fechaImpresion = { };
        //IMPRESION

        public Historial()
        {
            InitializeComponent();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            string[] fechaYhora=FechaUtil.obtenerFechaYHoraActual();
            refrescarLabels(fechaYhora);
            refrescarDGW(fechaYhora[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FechaUtil.validarFecha(textBox1.Text))
            {
                string[] fechaYhoraActual = FechaUtil.obtenerFechaYHoraActual();
                if (FechaUtil.completarConCeros(fechaYhoraActual[0]).Trim() == textBox1.Text.Trim())
                {
                    refrescarLabels(fechaYhoraActual);
                    refrescarDGW(fechaYhoraActual[0]);
                }
                else
                {
                    string[] fecha = { FechaUtil.quitarCeros(textBox1.Text), "22:00:00" };
                    
                    refrescarLabels(fecha);
                    refrescarDGW(fecha[0]);
                }
                
               
            }
            else
            {
               generadorDeMensajes.generarError(Mensajes.ERROR_FECHA);
            }
        }
        private void refrescarLabels(string[] fechaYhora)
        {
            textBox1.Text = FechaUtil.completarConCeros(fechaYhora[0]);
            label6.Text = FechaUtil.completarConCeros(fechaYhora[0]);
            label7.Text = fechaYhora[1].Substring(0, 5);


            //IMPRESION
            fechaImpresion = fechaYhora;
        }


        private void refrescarDGW(string fecha)
        {

            dataGridView1.Rows.Clear();
            ventas.Clear();
            ventas = ventasService.findByFecha(fecha);
            int ganancias = 0;
            for (int i = 0; i < ventas.Count; i++)
            {
                VentaDTO venta = ventas[i];
                dataGridView1.Rows.Add(venta.Id,
                                       venta.Descripcion,
                                       venta.Hora.Substring(0,5),
                                       venta.Unidades_vendidas.ToString(),
                                       PreciosUtil.generarPrecio(venta.PrecioVenta.ToString()),
                                       PreciosUtil.generarPrecio(venta.Ganancia));
                
                ganancias += int.Parse(venta.Ganancia);
            }
            refrescarGanancias(ganancias);
        }

        private void refrescarGanancias(int ganancias)
        {
            label8.Text = "$" + ganancias.ToString();
        }



        //IMPRESION
        private void button1_Click(object sender, EventArgs e)
        {
            indice = 0;
            pagina = 1;

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            indice = 0;
            pagina = 1;
            printDialog1.Document = printDocument2;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        private void imprimirComprobante(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font tituloFont = new Font("arial", 25, FontStyle.Bold);
            Font fechaFont1 = new Font("arial", 10, FontStyle.Bold);
            Font fechaFont2 = new Font("arial", 10);
            Font encabezadoFont = new Font("arial", 12, FontStyle.Bold);

            Pen pen = new Pen(Color.Black, 5);

            int width = 350;
            int height = 350;
            //ENCABEZADO
            if (pagina == 1)
            {
                e.Graphics.DrawString("Fecha: ", fechaFont1, Brushes.Black, new RectangleF(60, 40, width, height));
                e.Graphics.DrawString(FechaUtil.completarConCeros(fechaImpresion[0]), fechaFont2, Brushes.Black, new RectangleF(110, 40, width, height));

                e.Graphics.DrawString("Hora: ", fechaFont1, Brushes.Black, new RectangleF(60, 60, width, height));
                e.Graphics.DrawString(fechaImpresion[1].Substring(0, 5), fechaFont2, Brushes.Black, new RectangleF(110, 60, width, height));

                e.Graphics.DrawString("Ganancias: ", fechaFont1, Brushes.Black, new RectangleF(60, 80, width, height));
                e.Graphics.DrawString("$" + ventasUtil.calcularGanancias(ventas), fechaFont2, Brushes.Black, new RectangleF(140, 80, width, height));

                e.Graphics.DrawString("Ventas", tituloFont, Brushes.Black, new RectangleF(355, 95, width, height));
            }
            e.Graphics.DrawString("ID", encabezadoFont, Brushes.Black, new RectangleF(60, 140, width, height));
            e.Graphics.DrawString("PRODUCTO", encabezadoFont, Brushes.Black, new RectangleF(150, 140, width, height));
            e.Graphics.DrawString("HORA", encabezadoFont, Brushes.Black, new RectangleF(380, 140, width, height));
            e.Graphics.DrawString("U.VEND.", encabezadoFont, Brushes.Black, new RectangleF(455, 140, width, height));
            e.Graphics.DrawString("P.VENTA", encabezadoFont, Brushes.Black, new RectangleF(540, 140, width, height));
            e.Graphics.DrawString("GANANCIA", encabezadoFont, Brushes.Black, new RectangleF(650, 140, width, height));
            //ENCABEZADO


            e.Graphics.DrawLine(pen, 60, 160, 760, 160);

            int[] ejesYLV = { 160, 200 };
            //AMBOS EJES AUMENTAN DE 50 EN 50
            while (indice < ventas.Count)
            {

                VentaDTO venta = ventas[indice];

                //ITEMS = +20 ejesYLV
                e.Graphics.DrawLine(pen, 60, ejesYLV[0], 60, ejesYLV[1]);

                e.Graphics.DrawString(venta.Producto_id.ToString(), encabezadoFont, Brushes.Black, new RectangleF(70, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 140, ejesYLV[0], 140, ejesYLV[1]);

                string descripcion = (venta.Descripcion.Length > 25) ? ventasUtil.reducirCaracteresVenta(venta.Descripcion)
                                                                     : venta.Descripcion;

                e.Graphics.DrawString(descripcion, encabezadoFont, Brushes.Black, new RectangleF(150, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 370, ejesYLV[0], 370, ejesYLV[1]);

                e.Graphics.DrawString(venta.Hora.Substring(0, 5), encabezadoFont, Brushes.Black, new RectangleF(380, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 450, ejesYLV[0], 450, ejesYLV[1]);

                e.Graphics.DrawString(venta.Unidades_vendidas.ToString(), encabezadoFont, Brushes.Black, new RectangleF(455, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 530, ejesYLV[0], 530, ejesYLV[1]);

                e.Graphics.DrawString("$" + venta.PrecioVenta, encabezadoFont, Brushes.Black, new RectangleF(540, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 640, ejesYLV[0], 640, ejesYLV[1]);

                e.Graphics.DrawString("$" + venta.Ganancia, encabezadoFont, Brushes.Black, new RectangleF(650, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 760, ejesYLV[0], 760, ejesYLV[1]);

                //LINEA = +50 ejesYLV
                e.Graphics.DrawLine(pen, 60, ejesYLV[0] + 40, 760, ejesYLV[0] + 40);

                e.Graphics.DrawString("Pagina - " + pagina.ToString(), encabezadoFont, Brushes.Black, new RectangleF(400, 1120, width, height));



                ejesYLV[0] += 40;
                ejesYLV[1] += 40;
                if (indice == 22 || indice == 45 || indice == 68 || indice == 91 || indice == 114 || indice == 137 || indice == 159 || indice == 183 || indice == 205)
                {
                    indice++;
                    pagina++;
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    indice++;
                    e.HasMorePages = false;
                }
            }
        }

    }
    //IMPRESION

}
